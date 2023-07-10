using System;
using UnityEngine;

namespace Unit
{
    [CreateAssetMenu()]
    public class DamageSettings : EffectSettings
    {
        [field: SerializeField]
        public override float Value { get; set; }

        public override IEffectPresentor GetPresentor(IUnitModel unit, EffectModel model)
        {
            return new DamageEP(unit, model);
        }
    }
}
