using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit
{
    public interface IEffectSettings
    {
        float Value { get; }
        IEffectPresentor GetPresentor(PlayerModel player, EffectModel model);
    }
}
