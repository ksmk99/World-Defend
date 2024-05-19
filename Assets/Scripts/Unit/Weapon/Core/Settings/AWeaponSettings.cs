using System;
using System.Collections.Generic;
using Unit.Bullet;
using UnityEngine;

namespace Unit
{
    public abstract class AWeaponSettings : ScriptableObject, IWeaponSettings
    {
        [field: SerializeField]
        public float Distance { get; set; }
        [field: SerializeField]
        public float MinDistance { get; set; }
        [field: SerializeField]
        public LayerMask TargetLayer { get; set; }
        [field: SerializeField]
        public LayerMask BlockLayer { get; set; }
        [field: SerializeField]
        public List<EffectSettings> Effects { get; set; }
        [field: SerializeField]
        public BulletView BulletPrefab { get; set; }
        [field: SerializeField]
        public HitView HitPrefab { get; set; }
        [field: SerializeField]
        public int BulletPrespawnCount { get; set; }

        public abstract Type WeaponType { get; }
    }
}
