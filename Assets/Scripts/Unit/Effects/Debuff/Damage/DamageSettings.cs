using System;
using UnityEngine;

namespace Unit
{
    [CreateAssetMenu()]
    public class DamageSettings : IEffectSettings
    {
        [field: SerializeField]
        public float Value { get; }

        public IEffectPresentor GetPresentor(PlayerModel player, EffectModel model)
        {
            return new DamageEP(player, model);
        }
    }
}
