namespace Unit
{
    public class DamageEP : EffectPresenter
    {
        public DamageEP(IUnitModel unit, EffectModel model) : base(unit, model) { }

        public override void Update()
        {
            MakeAction();
            EndAction();
        }

        public override void MakeAction()
        {
            int value = (int)model.Settings.Value;
            player.Health.Damage(value);
        }
    }
}
