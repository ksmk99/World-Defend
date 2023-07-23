using Unit.Bullet;
using UnityEngine;

namespace Unit
{
    public class BulletModel
    {
        public IBulletSettings Settings { get; }
        public Transform Transform { get; }
        public Team Team { get; }

        public BulletModel(Transform transform, Team team, IBulletSettings settings)
        {
            Transform = transform;
            Team = team;
            Settings = settings;
        }
    }
}
