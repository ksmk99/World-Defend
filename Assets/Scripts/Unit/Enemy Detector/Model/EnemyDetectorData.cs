using System;
using UnityEngine;

namespace Unit
{
    [Serializable]
    public struct EnemyDetectorData
    {
        [field: SerializeField]
        public float RotationSpeed { get; private set; }
    }
}
