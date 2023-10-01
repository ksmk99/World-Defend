using System;
using UnityEngine;

namespace Unit.Bullet
{
    [Serializable]
    public class BulletSettings : IBulletSettings
    {
        [field: SerializeField]
        public float Speed { get; set; }
    }
}
