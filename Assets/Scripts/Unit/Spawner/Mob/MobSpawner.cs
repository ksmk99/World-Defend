using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit;
using UnityEngine;
using Zenject;

namespace Unit
{
    public class MobSpawner : IInitializable, IRoomResettable
    {
        public List<UnitView> ActiveUnits = new List<UnitView>(); 

        private readonly MobView.Factory factory;
        private readonly MobSpawnerSettings settings;
        private readonly ISpawnManager spawnManager;
        private readonly CustomPool<MobView> pool;
        private readonly Transform parent;
        private readonly int roomIndex;

        public MobSpawner(MobView.Factory factory, MobSpawnerSettings settings, ISpawnManager spawnManager, CustomPool<MobView> pool, Transform parent, int roomIndex)
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

        public async void Spawn()
        {
            for (int i = 0; i < settings.SpawnCount; i++)
            {
                MobView view = Create();
                SetStartSettings(view);

                await Task.Delay(5);
            }
        }

        public void Reset(SignalOnRoomReset signal)
        {
            if (this.roomIndex == signal.RoomIndex)
            {
                Spawn();
            }
        }


        private MobView Create()
        {
            MobView prefab = (MobView)spawnManager.GetPrefab();
            var id = (int)prefab.Type;
            MobView mob = pool.Create(id, prefab, factory.Create);
            return mob;
        }

        private void SetStartSettings(MobView view)
        {
            Vector3 randomPosition = new Vector3(
                UnityEngine.Random.Range(-settings.Offset.x, settings.Offset.x), 0,
                UnityEngine.Random.Range(-settings.Offset.z, settings.Offset.z));

            view.transform.position = randomPosition + settings.StartPoint.position;
            view.transform.SetParent(parent);
            view.gameObject.SetActive(true);

            view.GetPresenter().SetRoom(roomIndex);
            view.GetPresenter().Respawn();
            ActiveUnits.Add(view);  
        }

        public void Release(SignalOnMobDeath signal)
        {
            if (signal.RoomIndex == roomIndex)
            {
                signal.View.gameObject.SetActive(false);

                var id = signal.View.GetPoolID();
                pool.Release(id, (MobView)signal.View);
                ActiveUnits.Remove((MobView)signal.View);
            }
        }

        public void Release(SignalOnMobReset signal)
        {
            Release(new SignalOnMobDeath(signal.RoomIndex, signal.View));
        }
    }
}