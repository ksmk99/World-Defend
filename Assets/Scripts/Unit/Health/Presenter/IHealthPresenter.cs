namespace Unit
{
    public interface IHealthPresenter
    {
        void Heal(int count);
        void Damage(int count);
        void AutoHeal();
        bool IsDeath();
    }
}
