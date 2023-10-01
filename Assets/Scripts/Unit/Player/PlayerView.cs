using System.Collections.Generic;
using Unit;
using Zenject;

public class PlayerView : UnitView
{
    public override void Death()
    {
        return;
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
