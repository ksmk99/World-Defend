using UnityEngine;

namespace Unit
{
    public abstract class EffectSettings : ScriptableObject, IEffectSettings
    {
        public abstract float Value { get; set; }

        public abstract IEffectPresenter GetPresenter(IUnitModel player, EffectModel model);
    }
}
