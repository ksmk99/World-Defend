using Helpers;
using Unit;
using UnityEngine;
using Zenject;

namespace Unit
{
    internal class PlayerSpawner : IInitializable
    {
        private readonly PlayerView.Factory factory;
        private readonly PlayerSpawnerSettings settings;
        private readonly ISpawnManager spawnManager;
        private readonly CustomPool<PlayerView> pool;
        private readonly Transform parent;

        public PlayerSpawner(PlayerView.Factory factory, PlayerSpawnerSettings settings, ISpawnManager spawnManager, CustomPool<PlayerView> pool, Transform parent)
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
            view.OnDeath += Release;
        }

        public void Release(UnitView member)
        {
            member.gameObject.SetActive(false);
            member.OnDeath -= Release;

            var id = member.GetID();
            pool.Release(id, (PlayerView)member);
        }
    }
}