using System.Collections.Generic;
using Unit.Bullet;
using UnityEngine;

namespace Unit
{
    public class WeaponModel : IWeaponModel
    {
        public IWeaponSettings Settings { get; set; }
        public BulletView.Factory BulletPool { get; set; }
        public List<IBulletPresenter> Bullets { get; set; }

        public bool IsActing { get; set; }
        public bool CanUse { get; set; }

        public float NextUseTime { get; set; }
        public float ActionTimer { get; set; }
        public float TTL { get; set; }
        public Transform Target { get; set; }

        public WeaponModel(IWeaponSettings settings, BulletView.Factory factory)
        {
            Settings = settings;
            CanUse = true;
            BulletPool = factory;
            Bullets = new List<IBulletPresenter>();
        }
    }
}
