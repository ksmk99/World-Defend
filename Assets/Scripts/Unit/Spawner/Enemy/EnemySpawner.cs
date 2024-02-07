using Helpers;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using static Zenject.SignalSubscription;
using Random = UnityEngine.Random;

namespace Unit
{
    public class EnemySpawner : ITickable, IRoomResettable
    {
        private readonly EnemyView.Factory factory;
        private readonly EnemySpawnerSettings settings;
        private readonly CustomPool<EnemyView> pool;
        private readonly Transform parent;
        private readonly int roomIndex;
        private readonly ISpawnManager spawnManager;

        private float nextSpawnTime;
        private int spawnCount;

        public EnemySpawner(EnemyView.Factory factory, EnemySpawnerSettings settings, ISpawnManager spawnManager, CustomPool<EnemyView> pool, Transform parent, int roomIndex)
        {
            this.factory = factory;
            this.settings = settings;
            this.spawnManager = spawnManager;
            this.pool = pool;
            this.parent = parent;
            this.roomIndex = roomIndex;
        }

        public void Tick()
        {
            if (spawnCount >= settings.Count)
            {
                return;
            }

            if (nextSpawnTime <= Time.time)
            {
                EnemyView enemy = Create();
                SetStartSettings(enemy);

                spawnCount++;
                var delay = Random.Range(settings.SpawnRateMin, settings.SpawnRateMax);
                nextSpawnTime = Time.time + delay;
            }
        }

        public void Reset(SignalOnRoomReset signal)
        {
            if (this.roomIndex == signal.RoomIndex)
            {
                spawnCount = 0;
                nextSpawnTime = Time.time;
            }
        }

        private EnemyView Create()
        {
            EnemyView prefab = (EnemyView)spawnManager.GetPrefab();
            var id = (int)prefab.Type;
            EnemyView enemy = pool.Create(id, prefab, factory.Create);
            return enemy;
        }

        private void SetStartSettings(EnemyView enemy)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-settings.Offset.x, settings.Offset.x), 0,
                Random.Range(-settings.Offset.z, settings.Offset.z));

            enemy.transform.position = randomPosition + settings.StartPoint.position;
            enemy.transform.SetParent(parent);
            enemy.gameObject.SetActive(true);

            var presenter = enemy.GetPresenter();
            presenter.SetRoom(roomIndex);

            enemy.OnDeath += Release;
            enemy.Respawn();
        }

        public void Release(UnitView member)
        {
            member.gameObject.SetActive(false);

            var id = member.GetPoolID();
            pool.Release(id, (EnemyView)member);
            member.OnDeath -= Release;
        }
    }
}
