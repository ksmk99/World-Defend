using Unit.Bullet;
using UnityEngine;

namespace Unit
{
    public interface IWeaponModel
    {
        IWeaponSettings Settings { get; set; }
        BulletView.Factory BulletPool { get; set; }
        bool IsActing { get; set; }
        bool CanUse { get; set; }
        float NextUseTime { get; set; }
        float ActionTimer { get; set; }
        float TTL { get; set; }
        Transform Target { get; set; }
    }
}
