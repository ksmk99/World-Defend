using System;
using UnityEngine;

namespace Unit
{
    [Serializable]
    public class EnemySpawnerSettings
    {
        public int Count = 10;
        public float SpawnRateMin = 2f;
        public float SpawnRateMax = 2f;

        [field: SerializeField]
        public Vector3 Offset { get; set; }
        [field: SerializeField]
        public Transform StartPoint { get; set; }
        [field: SerializeField]
        public Vector3 MapSize { get; set; }
    }
}
