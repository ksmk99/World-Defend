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
    public void Init(PlayerPresenter presenter)
    {
        this.presenter = presenter;
    }

    public override bool TryAddEffects(List<EffectSettings> effects, Team team)
    {
        return presenter.AddEffects(effects, team);
    }

    public class Factory : PlaceholderFactory<UnityEngine.Object, PlayerView>
    {
    }
}
