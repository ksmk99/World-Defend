using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit
{
    public class EffectModel
    {
        public float ActionTimer { get; set; }
        public float TTL { get; set; }
        public IEffectSettings Settings { get; }

        public EffectModel(IEffectSettings settings)
        {
            Settings = settings;
        }
    }
}
