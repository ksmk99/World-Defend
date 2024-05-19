using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Unit
{
    [CreateAssetMenu()]
    public class HealthViewData : ScriptableObject
    {
        [field: SerializeField]
        public Sprite BarImage { get; set; }

        [field: SerializeField]
        public string Name { get; set; }
    }
}
