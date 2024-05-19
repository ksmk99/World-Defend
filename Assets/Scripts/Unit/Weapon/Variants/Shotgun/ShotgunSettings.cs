using System;
using UnityEngine;

namespace Unit
{
    [CreateAssetMenu(fileName = "ShotGun Settings", menuName = "Game/Weapon/ShotGun Settings")]
    public class ShotgunSettings : AWeaponSettings
    {
        public override Type WeaponType => typeof(ShotgunPresenter);

        public float ReloadTime;
        public float BulletInWave;
        public int WaveCount;
        public float WaveDelay;
        [Space]
        public float Angle;
    }
}
