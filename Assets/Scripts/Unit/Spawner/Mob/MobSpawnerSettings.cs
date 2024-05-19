using System;
using UnityEngine;

namespace Unit
{
    [Serializable]
    public class MobSpawnerSettings
    {
        public int SpawnCount;
        [field: SerializeField]
        public Vector3 Offset { get; set; }
        [field: SerializeField]
        public Transform StartPoint { get; set; }
    }
}
