using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit;
using UnityEngine;
using Zenject;

public class PlayerView : UnitView
{
    private PlayerPresentor presentor;

    public override void Death()
    {
        return;
    }

    [Inject]
    public void Init(PlayerPresentor presentor)
    {
        this.presentor = presentor;
    }

    public override bool TryAddEffects(List<EffectSettings> effects, Team team)
    {
        return presentor.AddEffects(effects, team);
    }
}
