﻿using Helpers;
using System.Threading.Tasks;
using Unit;
using UnityEngine;
using Zenject;

namespace Unit
{
    internal class PlayerSpawner : IInitializable, IRoomResettable
    {
        private readonly PlayerView.Factory factory;
        private readonly PlayerSpawnerSettings settings;
        private readonly ISpawnManager spawnManager;
        private readonly CustomPool<PlayerView> pool;
        private readonly Transform parent;
        private readonly int roomIndex;

        public PlayerSpawner(PlayerView.Factory factory, PlayerSpawnerSettings settings, ISpawnManager spawnManager, 
            CustomPool<PlayerView> pool, Transform parent, int roomIndex)
        {
            this.factory = factory;
            this.settings = settings;
            this.spawnManager = spawnManager;
            this.pool = pool;
            this.parent = parent;
            this.roomIndex = roomIndex;
        }

        public void Initialize()
        {
            Spawn();
        }

        public void Spawn()
        {
            PlayerView view = Create();
            SetStartSettings(view);
        }


        private PlayerView Create()
        {
            PlayerView prefab = (PlayerView)spawnManager.GetPrefab();
            PlayerView player = pool.Create(0, prefab, factory.Create);
            return player;
        }

        private void SetStartSettings(PlayerView view)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-settings.Offset.x, settings.Offset.x), 0,
                Random.Range(-settings.Offset.z, settings.Offset.z));

            view.transform.position = randomPosition + settings.StartPoint.position;
            view.transform.SetParent(parent);
            view.gameObject.SetActive(true);
            view.GetPresenter().SetRoom(roomIndex); 
        }

        public void Release(SignalOnPlayerDeath signal)
        {
            if (signal.RoomIndex == roomIndex)
            {
                signal.View.gameObject.SetActive(false);

                var id = signal.View.GetPoolID();
                pool.Release(id, (PlayerView)signal.View);
            }
        }

        public void Release(SignalOnPlayerReset signal)
        {
            Release(new SignalOnPlayerDeath(signal.RoomIndex, signal.View));
        }

        public async void Reset(SignalOnRoomReset signal)
        {
            if (this.roomIndex == signal.RoomIndex)
            {
                await Task.Delay(3000);

                Spawn();
            }
        }
    }
}