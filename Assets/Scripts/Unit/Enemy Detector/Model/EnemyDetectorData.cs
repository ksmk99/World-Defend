using System;
using UnityEngine;

namespace Unit
{
    [CreateAssetMenu()]
    public class EnemyDetectorData : ScriptableObject
    {
        [field: SerializeField]
        public float RotationSpeed { get; private set; }
    }
}
