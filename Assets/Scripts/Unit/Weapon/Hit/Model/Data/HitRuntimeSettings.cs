using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Unit
{
    public struct HitRuntimeSettings
    { 

        [field: SerializeField]
        public Vector3 Position { get; set; }
        [field: SerializeField]
        public Quaternion Rotation { get; set; }

        public HitRuntimeSettings(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }
}
