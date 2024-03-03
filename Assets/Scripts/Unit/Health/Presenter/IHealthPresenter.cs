using System;

namespace Unit
{
    public interface IHealthPresenter
    {
        event Action OnDeath;
        event Action OnDamage;

        void Heal(int count);
        void Damage(int count);
        bool IsDead();
        void Reset();
        void Disable();
    }
}
