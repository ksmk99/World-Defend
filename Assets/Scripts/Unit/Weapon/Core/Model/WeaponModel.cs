using Helpers;
using System.Collections.Generic;
using Unit.Bullet;
using UnityEngine;
using Zenject;

namespace Unit
{
    public class WeaponModel : IWeaponModel
    {
        public IWeaponSettings Settings { get; set; }
        public BulletView.Factory BulletPool { get; set; }
        public HitView.Factory HitPool { get; set; }
        public PoolParentFlag Parent { get; }
        public List<ADisposeView> Disposables { get; set; }

        public bool IsActing { get; set; }
        public bool CanUse { get; set; }

        public float NextUseTime { get; set; }
        public float ActionTimer { get; set; }
        public float TTL { get; set; }
        public Transform Target { get; set; }
        public SignalBus SignalBus { get; set; }
        public Team Team { get; internal set; }

        public WeaponModel(IWeaponSettings settings, BulletView.Factory bulletPool, HitView.Factory hitPool, SignalBus signalBus, PoolParentFlag parent)
        {
            Settings = settings;
            BulletPool = bulletPool;
            HitPool = hitPool;

            Parent = parent;
            SignalBus = signalBus;

            CanUse = true;
            Disposables = new List<ADisposeView>();
        }
    }
}
