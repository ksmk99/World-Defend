using System;
using UnityEngine;
using Zenject;

namespace Unit
{
    public class HitView : ADisposeView, IPoolable<HitRuntimeSettings, IMemoryPool>, IHitView
    {
        [SerializeField]

        private IMemoryPool _pool;
        private ParticleSystem[] particles;

        public override event Action<ADisposeView> OnDispose;
        public event Action<HitRuntimeSettings> OnReinitialize;

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

        public void OnSpawned(HitRuntimeSettings settings, IMemoryPool memoryPool)
        {
            if (particles == null)
            {
                particles = GetComponentsInChildren<ParticleSystem>();
            }

            foreach (ParticleSystem particle in particles)
            {
                particle.Play();
            }

            _pool = memoryPool;
            OnReinitialize?.Invoke(settings);
        }

        public class Factory : PlaceholderFactory<HitRuntimeSettings, HitView>
        {

        }
    }
}