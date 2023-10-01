using System.Collections.Generic;
using Unity.VisualScripting;
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
        private readonly Transform parent;
        private float nextSpawnTime;

        private Dictionary<int, Queue<EnemyView>> memberPool = new Dictionary<int, Queue<EnemyView>>();
        public EnemySpawner(EnemyView.Factory factory, EnemySpawnerSettings settings, ISpawnManager spawnManager, Transform parent)
        {
            this.factory = factory;
            this.settings = settings;
            this.spawnManager = spawnManager;
            this.parent = parent;
        }

        public void Tick()
        {
            if (nextSpawnTime <= Time.time)
            {
                EnemyView enemy = CreateEnemy();
                SetStartSettings(enemy);

                nextSpawnTime = Time.time + settings.SpawnRate;
            }
        }

        private void SetStartSettings(EnemyView enemy)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-settings.Offset.x, settings.Offset.x), 0,
                Random.Range(-settings.Offset.z, settings.Offset.z));

            enemy.transform.position = randomPosition;
            enemy.transform.SetParent(parent);
            enemy.gameObject.SetActive(true);   
            enemy.Respawn();
        }

        private EnemyView CreateEnemy()
        {
            EnemyView prefab = (EnemyView)spawnManager.GetPrefab();
            var id = (int)prefab.Type;
            if (!memberPool.ContainsKey(id))
            {
                memberPool.Add(id, new Queue<EnemyView>());
            }

            if (memberPool[id].Count == 0)
            {
                EnemyView member = factory.Create(prefab);
                member.OnDeath += Release;
                return member;
            }

            return memberPool[id].Dequeue();
        }

        public void Release(EnemyView member)
        {
            Debug.Log("Death");
            memberPool[(int)member.Type].Enqueue(member);
            member.gameObject.SetActive(false);
        }
    }
}
