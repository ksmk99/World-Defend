using System.Collections.Generic;
using Unit;
using UnityEngine;

public abstract class UnitView : MonoBehaviour
{
    protected UnitPresenter presenter;

    public abstract bool TryAddEffects(List<EffectSettings> effects, Team team);

    public abstract void Death();
    public virtual UnitPresenter GetPresenter()
    {
        return presenter;
    }

    public virtual Team GetTeam()
    {
        return presenter.Team;
    }
}
