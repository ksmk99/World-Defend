using System;
using System.Collections.Generic;
using Unit.Bullet;
using UnityEngine;

namespace Unit
{
    [CreateAssetMenu(fileName = "Weapon Settings", menuName = "Game/WeaponSettings")]
    public class WeaponSettings : ScriptableObject, IWeaponSettings
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

        public Type WeaponType => typeof(WeaponPresenter);

        public BulletView BulletPrefab;
        public HitView HitPrefab;

        public int BulletCount;
        public float ReloadTime;
        public float BulletDelay;
    }
}
