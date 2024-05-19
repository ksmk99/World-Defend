using UnityEngine;

namespace Unit
{
    public interface IWeaponPresenter
    {
        IWeaponSettings Settings { get; }

        bool IsReloading();
        bool SetAction(Transform transform, Team team);
        void Attack(Transform transform, bool isDead, Team team);
        void Update(Transform transform, bool isDead, Team team);
        void Reset();
        Transform GetTarget();
        void Disable();
    }
}
