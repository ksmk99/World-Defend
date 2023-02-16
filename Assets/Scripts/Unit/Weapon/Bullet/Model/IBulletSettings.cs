using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit.Bullet
{
    public interface IBulletSettings
    {
        [field: SerializeField]
        public float Speed { get; set; }
        [field: SerializeField]
        public int Damage { get; set; }

    }
}
