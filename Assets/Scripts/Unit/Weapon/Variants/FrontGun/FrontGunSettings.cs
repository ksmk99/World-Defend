using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit.Bullet;
using Unit;
using UnityEngine;

namespace Unit
{
    [CreateAssetMenu(fileName = "FrontGun Settings", menuName = "Game/Weapon/FrontGun Settings")]
    public class FrontGunSettings : AWeaponSettings
    {
        public override Type WeaponType => typeof(FrontGunPresenter);

        public int BulletCount;
        public float ReloadTime;
        public float BulletDelay;
    }
}
