﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit
{
    public class HealthModel
    {
        public HealthSettings Settings { get; private set; }

        public int Health;
        public float NextHealTime;

        public HealthModel(HealthSettings settings)
        {
            Settings = settings;
            Health = settings.MaxHealth;
        }
    }
}
