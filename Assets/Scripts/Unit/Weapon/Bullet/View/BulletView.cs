using System;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Unit.Bullet
{
    public class BulletView : ADisposeView, IPoolable<BulletRuntimeSettings, IMemoryPool>
    {
        private ParticleSystem[] particles;

        private IMemoryPool _pool;

        private BulletPresenter presenter;

        public override event Action<ADisposeView> OnDispose;

        [Inject]
        public void Init(BulletPresenter presenter)
        {
            this.presenter = presenter;
        }

        public BulletPresenter GetPresenter()
        {
            return presenter;
        }

        public override void Dispose()
        {
            if (particles == null)
            {
                particles = GetComponentsInChildren<ParticleSystem>();
            }

            foreach (ParticleSystem particle in particles)
            {
                particle.Stop();
            }

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
            presenter.Reinitialize(p1);
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Invoke " + other);
            presenter.Collide(other);
        }

        public class Factory : PlaceholderFactory<BulletRuntimeSettings, BulletView>
        {

        }
    }
}