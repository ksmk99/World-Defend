using UnityEngine;

namespace Unit
{
    public interface IWeaponModel
    {
        public IWeaponSettings Settings { get; set; }

        public bool IsActing { get; set; }
        public bool CanUse { get; set; }
        public float NextUseTime { get; set; }
        public float ActionTimer { get; set; }
        public float TTL { get; set; }
        public Transform Target { get; set; }
    }
}
