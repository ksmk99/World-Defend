namespace Unit
{
    public interface IHealthPresentor
    {
        void Heal(int count);
        void Damage(int count);
        void AutoHeal();
        bool IsDeath();
    }
}
