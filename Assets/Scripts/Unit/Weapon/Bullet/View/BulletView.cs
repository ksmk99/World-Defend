using System;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using Zenject;
using static Zenject.CheatSheet;

namespace Unit.Bullet
{
    public class BulletView : MonoBehaviour, 
        IBulletView, 
        IPoolable<IBulletSettings, BulletRuntimeSettings, IMemoryPool>, 
        IDisposable
    {
        private IMemoryPool pool;

        public event Action<Collider> OnCollide;
        public event Action<IBulletSettings, BulletRuntimeSettings> OnDataUpdate;

        public void OnTriggerEnter(Collider other)
        {
            OnCollide?.Invoke(other);
        }

        public void OnSpawned(IBulletSettings p1, BulletRuntimeSettings p2, IMemoryPool pool)
        {
            this.pool = pool;

            OnDataUpdate?.Invoke(p1, p2);

            //gameObject.SetActive(true);
        }

        public void OnDespawned()
        {
            pool = null;

            //gameObject.SetActive(false);
        }

        public void Dispose()
        {
            pool.Despawn(this);
        }

        public class Pool : MonoMemoryPool<IBulletSettings, BulletRuntimeSettings, BulletView>
        {
        }
    }

    //public interface IMyFooFactory : IFactory<BulletView>
    //{
    //}

    //public class BulletFactory : IFactory<BulletView>
    //{
    //    readonly DiContainer _container;

    //    public BulletFactory(DiContainer container)
    //    {
    //        _container = container;
    //    }

    //    //public BulletView Create(UnityEngine.Object prefab, IBulletSettings param2, BulletRuntimeSettings param3)
    //    //{
    //    //    var result = _container.InstantiatePrefabForComponent<BulletView>(prefab);
    //    //    var model = _container.Resolve<BulletModel>();
    //    //    var presentor = _container.Resolve<BulletPresentor>();
    //    //    model.Init(param2, param3);
    //    //    presentor.Init(model, result);
    //    //    return result;
    //    //}

    //    //public BulletView Create()
    //    //{
    //    //    var result = _container.InstantiatePrefabForComponent<BulletView>(prefab);
    //    //    var model = _container.Resolve<BulletModel>();
    //    //    var presentor = _container.Resolve<BulletPresentor>();
    //    //    model.Init(param2, param3);
    //    //    presentor.Init(model, result);
    //    //    return result;
    //    //}

    //    public BulletView Create(UnityEngine.Object param)
    //    {
    //        return _container.InstantiatePrefabForComponent<BulletView>(param);
    //    }

    //    public BulletView Create()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}