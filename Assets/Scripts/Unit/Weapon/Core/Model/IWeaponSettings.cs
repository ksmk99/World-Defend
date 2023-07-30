using System;
using System.Collections.Generic;
using Unit.Bullet;
using UnityEngine;

namespace Unit
{
    public interface IWeaponSettings
    {
        float Distance { get; }
        LayerMask TargetLayer { get; }
        Type WeaponType { get; }
        BulletSettings BulletSettings { get; }
        List<EffectSettings> Effects { get; }
    }
}
