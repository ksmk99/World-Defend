using System;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Unit.Bullet
{
    public class BulletView : ADisposeView, IBulletView, IPoolable<BulletRuntimeSettings, IMemoryPool>
    {
        private ParticleSystem[] particles;

        private IMemoryPool _pool;

        private BulletPresenter presenter;

        public event Action<Collider> OnCollide;
        public override event Action<ADisposeView> OnDispose;
        public event Action<BulletRuntimeSettings> OnReinitialize;

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
            OnReinitialize?.Invoke(p1);

            PlayParticles();
        }

        private async void PlayParticles()
        {
            if (particles == null)
            {
                particles = GetComponentsInChildren<ParticleSystem>();
            }

            await Task.Delay(10);

            foreach (ParticleSystem particle in particles)
            {
                particle?.Play();
            }
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