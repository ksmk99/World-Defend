using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Unit
{
    [Serializable]
    public class HealthSettings
    {
        public float MaxHealth;
        [Space]
        public bool IsAutoHeal;
        public float HealDelay;
        public float HealRate;
        public int HealCount;
    }
}
