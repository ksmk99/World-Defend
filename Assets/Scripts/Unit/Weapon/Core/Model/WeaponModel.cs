using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit.Bullet;
using Unity.VisualScripting;
using UnityEngine;
using static Zenject.CheatSheet;

namespace Unit
{
    public class WeaponModel : IWeaponModel
    {
        public IWeaponSettings Settings { get; set; }
        public BulletView.Factory BulletFactory { get;  set; }
        public List<BulletView> Bullets { get; set; }
        public Team Team { get;  set; }

        public bool IsActing { get; set; }
        public bool CanUse { get; set; }

        public float NextUseTime { get; set; }
        public float ActionTimer { get; set; }
        public float TTL { get; set; }
        public Transform Target { get; set; }

        public WeaponModel(IWeaponSettings settings, Team team,
            BulletView.Factory factory)
        {
            Settings = settings;
            CanUse = true;
            BulletFactory = factory;
            Team = team;
            Bullets = new List<BulletView>();
        }
    }
}
