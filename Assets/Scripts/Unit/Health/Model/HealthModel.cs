using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit
{
    public class HealthModel
    {
        public HealthSettings Settings { get; private set; }

        public float Health;
        public float NextHealTime;

        public bool IsDeath;

        public HealthModel(HealthSettings settings)
        {
            Settings = settings;
            Health = settings.MaxHealth;
        }
    }
}
