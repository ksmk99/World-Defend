using System;
using UnityEngine;
using Zenject;
using static Zenject.SignalSubscription;

namespace Unit.Bullet
{
    public class BulletView : MonoBehaviour, IBulletView, IPoolable<BulletRuntimeSettings, IMemoryPool>, IDisposable
    {
        private IMemoryPool _pool;

        public event Action<Collider> OnCollide;
        public event Action<BulletView> OnDispose;
        public event Action<BulletRuntimeSettings> OnReinitialize;

        public void Dispose()
        {
            _pool.Despawn(this);
            OnDispose?.Invoke(this);
        }

        public void OnDespawned()
        {
            _pool = null;
        }

        public void OnSpawned(BulletRuntimeSettings p1, IMemoryPool p2)
        {
            _pool = p2;
            OnReinitialize?.Invoke(p1);
        }

        public void OnTriggerEnter(Collider other)
        {
            OnCollide?.Invoke(other);
        }

        public class Factory : PlaceholderFactory<BulletRuntimeSettings, BulletView>
        {

        }
    }
}