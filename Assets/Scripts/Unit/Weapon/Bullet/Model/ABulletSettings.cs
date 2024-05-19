using System;
using Unit.Bullet;
using UnityEngine;

namespace Unit
{
    public abstract class ABulletSettings : ScriptableObject, IBulletSettings
    {
        [field: SerializeField]
        public float Speed { get; set; }
        public abstract Type BulletType { get; }
    }
}
