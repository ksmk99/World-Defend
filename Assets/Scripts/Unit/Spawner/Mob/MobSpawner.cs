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
    public class MobSpawner : IInitializable
    {
        private readonly MobView.Factory factory;
        private readonly MobSpawnerSettings settings;
        private readonly ISpawnManager spawnManager;
        private readonly CustomPool<MobView> pool;
        private readonly Transform parent;

        public MobSpawner(MobView.Factory factory, MobSpawnerSettings settings, ISpawnManager spawnManager, CustomPool<MobView> pool, Transform parent)
        {
            this.factory = factory;
            this.settings = settings;
            this.spawnManager = spawnManager;
            this.pool = pool;
            this.parent = parent;
        }

        public void Initialize()
        {
            Spawn();
        }

        public void Spawn()
        {
            for (int i = 0; i < settings.SpawnCount; i++)
            {
                MobView view = Create();
                SetStartSettings(view);
            }
        }


        private MobView Create()
        {
            MobView prefab = (MobView)spawnManager.GetPrefab();
            var id = (int)prefab.Type;
            MobView enemy = pool.Create(id, prefab, factory.Create);
            return enemy;
        }

        private void SetStartSettings(MobView view)
        {
            Vector3 randomPosition = new Vector3(
                UnityEngine.Random.Range(-settings.Offset.x, settings.Offset.x), 0,
                UnityEngine.Random.Range(-settings.Offset.z, settings.Offset.z));

            view.transform.position = randomPosition;
            view.transform.SetParent(parent);
            view.gameObject.SetActive(true);
            view.OnDeath += Release;
        }

        public void Release(UnitView member)
        {
            member.gameObject.SetActive(false);
            member.OnDeath -= Release;

            var id = member.GetID();
            pool.Release(id, (MobView)member);
        }
    }
}