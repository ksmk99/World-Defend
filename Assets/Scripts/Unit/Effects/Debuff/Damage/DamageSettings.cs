using UnityEngine;

namespace Unit
{
    [CreateAssetMenu()]
    public class DamageSettings : EffectSettings
    {
        [field: SerializeField]
        public override float Value { get; set; }

        public override IEffectPresenter GetPresenter(IUnitModel unit, EffectModel model)
        {
            return new DamageEP(unit, model);
        }
    }
}
