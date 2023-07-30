using System;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using Zenject;
using static Zenject.CheatSheet;

namespace Unit.Bullet
{
    public class BulletView : MonoBehaviour,
        IBulletView,
        IPoolable<IMemoryPool>,
        IDisposable
    {
        private IMemoryPool pool;

        public event Action<Collider> OnCollide;

        public void OnTriggerEnter(Collider other)
        {
            OnCollide?.Invoke(other);
        }

        public void OnSpawned(IMemoryPool pool)
        {
            this.pool = pool;

            gameObject.SetActive(true);
        }

        public void OnDespawned()
        {
            pool = null;

            gameObject.SetActive(false);
        }

        public void Dispose()
        {
            pool.Despawn(this);
        }

        public class Factory : PlaceholderFactory<UnityEngine.Object, IBulletSettings, BulletRuntimeSettings, BulletView>
        {
        }

        public class Pool : MemoryPool<UnityEngine.Object, IBulletSettings, BulletRuntimeSettings, BulletView>
        {

        }
    }

    public class BulletFactory : IFactory<BulletView>
    {
        readonly DiContainer _container;

        public BulletFactory(DiContainer container)
        {
            _container = container;
        }

        public BulletView Create(UnityEngine.Object prefab, IBulletSettings param2, BulletRuntimeSettings param3)
        {
            var result = _container.InstantiatePrefabForComponent<BulletView>(prefab);
            var model = _container.Resolve<BulletModel>();
            var presentor = _container.Resolve<BulletPresentor>();
            model.Init(param2, param3);
            presentor.Init(model, result);
            return result;
        }

        public BulletView Create()
        {
            throw new NotImplementedException();
        }
    }
}