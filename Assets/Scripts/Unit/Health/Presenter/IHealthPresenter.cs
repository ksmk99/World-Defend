using System;

namespace Unit
{
    public interface IHealthPresenter
    {
        event Action OnDeath;

        void Heal(int count);
        void Damage(int count);
        bool IsDead();
        void Reset();
    }
}
