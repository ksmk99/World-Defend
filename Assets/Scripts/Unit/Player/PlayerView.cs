using System;
using System.Collections.Generic;
using Unit;
using Zenject;

public class PlayerView : UnitView
{
    public override Action<UnitView> OnDeath { get; set; }

    public override void Death()
    {
        return;
    }

    public override int GetID()
    {
        return 0;
    }

    [Inject]
    public void Init(PlayerPresentor presentor)
    {
        this.presenter = presentor;
    }

    public override bool TryAddEffects(List<EffectSettings> effects, Team team)
    {
        return presenter.AddEffects(effects, team);
    }
}
