using UnityEngine;

namespace Unit
{
    public interface IWeaponPresenter
    {
        bool IsReloading();
        bool SetAction(Transform transform, Team team);
        void Attack(Transform transform, bool isDead, Team team);
        void Update(Transform transform, bool isDead, Team team);
        void Reset();
    }
}
