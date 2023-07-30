using System.Collections.Generic;
using Unit.Bullet;
using UnityEngine;

namespace Unit
{
    public class WeaponModel : IWeaponModel
    {
        public IWeaponSettings Settings { get; set; }
        public BulletFactory BulletFactory { get; set; }
        public List<IBulletPresentor> Bullets { get; set; }

        public bool IsActing { get; set; }
        public bool CanUse { get; set; }

        public float NextUseTime { get; set; }
        public float ActionTimer { get; set; }
        public float TTL { get; set; }
        public Transform Target { get; set; }

        public WeaponModel(IWeaponSettings settings, BulletFactory factory)
        {
            Settings = settings;
            CanUse = true;
            BulletFactory = factory;
            Bullets = new List<IBulletPresentor>();
        }
    }
}
