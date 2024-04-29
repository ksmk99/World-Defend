using System;
using UnityEngine;

namespace Unit
{
    [CreateAssetMenu()]
    public class HealthSettings : ScriptableObject
    {
        public float MaxHealth;
        [Space]
        public bool IsAutoHeal;
        public float HealDelay;
        public float HealRate;
        public int HealCount;
    }
}
