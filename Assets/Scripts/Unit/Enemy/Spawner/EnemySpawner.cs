using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Unit
{
    public class EnemySpawner : ITickable
    {
        private readonly EnemyView.Factory factory;
        private readonly EnemySpawnerSettings settings;
        private float nextSpawnTime;

        private HealthSettings healthSettings;
        private WeaponSettings weaponSettings;

        public EnemySpawner(EnemyView.Factory factory, EnemySpawnerSettings settings)
            //HealthSettings healthSettings, WeaponSettings weaponSettings)
        {
            this.factory = factory;
            this.settings = settings;
            //this.healthSettings = healthSettings;
            //this.weaponSettings = weaponSettings;
        }

        public void Tick()
        {
            if (nextSpawnTime <= Time.time)
            {
                EnemyView enemy = factory.Create();
                enemy.Respawn();
                Vector3 randomPosition = new Vector3(
                    Random.Range(-settings.Offset.x, settings.Offset.x), 0,
                    Random.Range(-settings.Offset.z, settings.Offset.z));
                enemy.transform.position = randomPosition;

                nextSpawnTime = Time.time + settings.SpawnRate;
            }
        }
    }
}
