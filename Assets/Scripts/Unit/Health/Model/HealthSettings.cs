using System;
using UnityEngine;

namespace Unit
{
    [Serializable]
    public struct HealthSettings
    {
        public float MaxHealth;
        [Space]
        public bool IsAutoHeal;
        public float HealDelay;
        public float HealRate;
        public int HealCount;
    }
}
