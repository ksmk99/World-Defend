using System;
using UnityEngine;
using Cinemachine;

namespace Unit
{
    [Serializable]
    public class PlayerSpawnerSettings
    {
        [field: SerializeField]
        public Vector3 Offset { get; set; }
        [field: SerializeField]
        public Transform StartPoint { get; set; }

        [field: SerializeField]
        public CinemachineVirtualCamera CMCamera { get; set; }
    }
}
