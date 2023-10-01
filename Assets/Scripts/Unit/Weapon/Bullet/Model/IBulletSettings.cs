using UnityEngine;

namespace Unit.Bullet
{
    public interface IBulletSettings
    {
        [field: SerializeField]
        public float Speed { get; set; }
    }
}
