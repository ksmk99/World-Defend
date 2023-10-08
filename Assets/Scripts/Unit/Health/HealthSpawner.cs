﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Unit
{
    public class HealthSpawner 
    {
        private readonly HealthView.Factory factory;
        private readonly Transform parent;

        public HealthSpawner(HealthView.Factory factory, Transform parent)
        {
            this.factory = factory;
            this.parent = parent;
        }

        public HealthView Spawn()
        {
            var view = factory.Create();
            view.transform.SetParent(parent);
            return view;
        }
    }
}
