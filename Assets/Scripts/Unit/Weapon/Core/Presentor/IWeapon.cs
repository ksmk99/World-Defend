using UnityEngine;

namespace Unit
{
    public interface IWeapon
    {
        bool IsReloading();
        bool SetAction();
        void Attack();
        void Update();   
    }
}
