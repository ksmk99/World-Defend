using System;
using UnityEngine;

namespace Unit.Bullet
{
    [CreateAssetMenu(fileName = "Default Bullet", menuName = "Game/Bullet/Default Bullet")]
    public class DefaultBulletSettings : ABulletSettings
    {
        public override Type BulletType => typeof(DefaultBulletPresenter);
    }
}
