using System;
using System.Collections.Generic;
using Unit;
using UnityEngine;

public abstract class UnitView : MonoBehaviour
{
    protected UnitPresenter presenter;

    public abstract Action<UnitView> OnDeath { get; set; }

    public abstract bool TryAddEffects(List<EffectSettings> effects, Team team);

    public abstract int GetID();
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
