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

        public MobSpawner(MobView.Factory factory, MobSpawnerSettings settings)
        {
            this.factory = factory;
            this.settings = settings;
        }

        public void Initialize()
        {
            Spawn();
        }

        public void Spawn()
        {
            for (int i = 0; i < settings.SpawnCount; i++)
            {
                MobView mob = factory.Create();
                Vector3 randomPosition = new Vector3(
                    UnityEngine.Random.Range(-settings.Offset.x, settings.Offset.x), 0,
                    UnityEngine.Random.Range(-settings.Offset.z, settings.Offset.z));
                mob.transform.position = randomPosition;
            }
        }
    }
}