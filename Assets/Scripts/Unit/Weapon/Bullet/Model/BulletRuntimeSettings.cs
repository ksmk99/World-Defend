using System.Collections.Generic;
using UnityEngine;

namespace Unit.Bullet
{
    public struct BulletRuntimeSettings
    {
        public float Distance;

        public Quaternion Rotation;
        public Vector3 Position;

        public Team Team;

        public List<EffectSettings> Effects;

        public BulletRuntimeSettings(float distance, Quaternion rotation, Vector3 position, Team team,
            List<EffectSettings> effects)
        {
            Distance = distance;
            Rotation = rotation;
            Position = position;
            Team = team;
            Effects = effects;
        }
    }
}
