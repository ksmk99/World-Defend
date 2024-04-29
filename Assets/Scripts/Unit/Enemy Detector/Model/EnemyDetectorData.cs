using System;
using UnityEngine;

namespace Unit
{
    [Serializable]
    public class EnemyDetectorData 
    {
        [field: SerializeField]
        public float RotationSpeed { get; private set; }
    }
}
