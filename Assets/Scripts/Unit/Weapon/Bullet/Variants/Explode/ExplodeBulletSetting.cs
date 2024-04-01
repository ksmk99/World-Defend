using System;
using UnityEngine;

namespace Unit.Bullet
{
    [CreateAssetMenu(fileName = "Explode Bullet", menuName = "Game/Bullet/Explode Bullet")]
    public class ExplodeBulletSetting : ABulletSettings
    {
        [field: SerializeField]
        public float Radius { get; set; }
        public override Type BulletType => typeof(ExplodeBulletPresenter);
    }
}
