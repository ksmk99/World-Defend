namespace Unit
{
    public interface IEffectSettings
    {
        float Value { get; }
        IEffectPresenter GetPresenter(IUnitModel player, EffectModel model);
    }
}
