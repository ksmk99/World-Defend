using System.Collections.Generic;
using Unit;
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
