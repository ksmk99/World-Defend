using UnityEngine;

namespace Unit
{
    public interface IWeaponPresentor
    {
        bool IsReloading();
        bool SetAction(Transform transform);
        void Attack(Transform transform);
        void Update(Transform transform);   
    }
}
