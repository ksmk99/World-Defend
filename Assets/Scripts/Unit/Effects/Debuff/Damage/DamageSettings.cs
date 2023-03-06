using System;
using UnityEngine;

namespace Unit
{
    [CreateAssetMenu()]
    public class DamageSettings : IEffectSettings
    {
        [field: SerializeField]
        public float Value { get; }

        public IEffectPresentor GetPresentor(IUnitModel unit, EffectModel model)
        {
            return new DamageEP(unit, model);
        }
    }
}
