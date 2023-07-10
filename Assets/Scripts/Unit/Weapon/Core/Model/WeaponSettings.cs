using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit.Bullet;
using UnityEngine;

namespace Unit
{
    [CreateAssetMenu()]
    public class WeaponSettings : ScriptableObject, IWeaponSettings
    {
        [field: SerializeField]
        public float Distance { get; set; }
        [field: SerializeField]
        public LayerMask TargetLayer { get; set; }
        [field: SerializeField]
        public List<EffectSettings> Effects { get; set; }

        public Type WeaponType => typeof(WeaponPresentor);

        public BulletView BulletPrefab;
        public int BulletCount;
        public float ReloadTime;
        public float BulletDelay;
    }
}
