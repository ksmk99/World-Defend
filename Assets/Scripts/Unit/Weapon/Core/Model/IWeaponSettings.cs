using System;
using System.Collections.Generic;
using Unit.Bullet;
using UnityEngine;

namespace Unit
{
    public interface IWeaponSettings
    {
        float Distance { get; }
        float MinDistance { get; }  
        LayerMask TargetLayer { get; }
        LayerMask BlockLayer { get; }
        Type WeaponType { get; }
        List<EffectSettings> Effects { get; }
    }
}
