using System;
using Zenject;

namespace Unit
{
    public interface IHitView
    {
        event Action<HitRuntimeSettings> OnReinitialize;

        void Dispose();
        void OnDespawned();
        void OnSpawned(HitRuntimeSettings settings, IMemoryPool memoryPool);
    }
}