using UnityEngine;

namespace Unit
{
    public abstract class EffectSettings : ScriptableObject, IEffectSettings
    {
        public abstract float Value { get; set; }

        public abstract IEffectPresentor GetPresentor(IUnitModel player, EffectModel model);
    }
}
