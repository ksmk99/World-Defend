using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Unit
{
    public interface IEffectSettings
    {
        float Value { get; }
        IEffectPresentor GetPresentor(IUnitModel player, EffectModel model);
    }
}
