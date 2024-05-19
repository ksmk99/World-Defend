using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Unit
{
    [CreateAssetMenu(fileName = "Round Attack Settings", menuName = "Game/Weapon/Round Attack Settings")]
    public class RoundAttackSettings : AWeaponSettings
    {
        public override Type WeaponType => typeof(RoundAttackPresenter);

        public float ReloadTime = 1.5f;
        public float AttackTime = 0.5f;
    }
}
