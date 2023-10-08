namespace Unit
{
    public interface IHealthPresenter
    {
        void Heal(int count);
        void Damage(int count);
        bool IsDead();
        void Reset();
    }
}
