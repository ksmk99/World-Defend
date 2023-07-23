using UnityEngine;

namespace Unit.Bullet
{
    [CreateAssetMenu()]
    public class BulletSettings : ScriptableObject, IBulletSettings
    {
        [field: SerializeField]
        public float Speed { get; set; }
        [field: SerializeField]
        public int Damage { get; set; }
    }
}
