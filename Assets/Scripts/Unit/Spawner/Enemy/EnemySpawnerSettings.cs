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
        public Vector3 Offset;
    }
}
