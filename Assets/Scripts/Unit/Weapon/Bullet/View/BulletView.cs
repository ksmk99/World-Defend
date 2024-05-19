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

        private ABulletPresenter presenter;

        public override event Action<ADisposeView> OnDispose;

        [Inject]
        public void Init(ABulletPresenter presenter)
        {
            this.presenter = presenter;
        }

        public ABulletPresenter GetPresenter()
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

        public void OnTriggerEnter(Collider other)
        {
            presenter.Collide(other.gameObject);
        }

        public class Factory : PlaceholderFactory<BulletRuntimeSettings, BulletView>
        {

        }
    }
}