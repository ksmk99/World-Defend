using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Unit
{
    public class WeaponModel : IWeaponModel
    {
        public IWeaponSettings Settings { get; set; }

        public bool IsActing { get; set; }
        public bool CanUse { get; set; }

        public float NextUseTime { get; set; }
        public float ActionTimer { get; set; }
        public float TTL { get; set; }
        public Transform Target { get; set; }

        public WeaponModel(IWeaponSettings settings)
        {
            Settings = settings;
            CanUse = true;  
        }
    }
}
