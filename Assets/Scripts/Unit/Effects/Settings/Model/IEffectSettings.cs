namespace Unit
{
    public interface IEffectSettings
    {
        float Value { get; }
        IEffectPresentor GetPresentor(IUnitModel player, EffectModel model);
    }
}
