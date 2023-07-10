using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Unit
{
    public interface IWeaponSettings
    {
        public float Distance { get; }
        public LayerMask TargetLayer { get; }
        public Type WeaponType { get; }
        public List<EffectSettings> Effects { get; }
    }
}
