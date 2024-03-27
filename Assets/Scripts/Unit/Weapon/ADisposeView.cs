using System;
using UnityEngine;

namespace Unit
{
    public abstract class ADisposeView : MonoBehaviour, IDisposable
    {
        public abstract event Action<ADisposeView> OnDispose;

        public abstract void Dispose();
    }
}