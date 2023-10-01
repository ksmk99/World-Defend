using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Unit
{
    public class EnemySpawner : ITickable
    {
        private readonly EnemyView.Factory factory;
        private readonly EnemySpawnerSettings settings;
        private readonly ISpawnManager spawnManager;
        private float nextSpawnTime;

        public EnemySpawner(EnemyView.Factory factory, EnemySpawnerSettings settings, ISpawnManager spawnManager)
        {
            this.factory = factory;
            this.settings = settings;
            this.spawnManager = spawnManager;
        }

        public void Tick()
        {
            if (nextSpawnTime <= Time.time)
            {
                EnemyView enemy = factory.Create(spawnManager.GetPrefab());
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
