using System;
using Unit.Bullet;
using UnityEngine;

namespace Unit
{
    public class BulletModel
    {
        public IBulletSettings Settings { get; private set; }
        public BulletRuntimeSettings RuntimeSettings { get; private set; }
        public Team Team => RuntimeSettings.Team;

        public bool CanCollide { get; set; }

        public BulletModel(IBulletSettings settings)
        {
            Settings = settings;
        }

        public void Init(BulletRuntimeSettings param3)
        {
            RuntimeSettings = param3;

            CanCollide = true;
        }
    }
}
