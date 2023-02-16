using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
