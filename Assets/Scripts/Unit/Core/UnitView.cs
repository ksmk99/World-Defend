using System.Collections.Generic;
using Unit;
using UnityEngine;

public abstract class UnitView : MonoBehaviour
{
    protected UnitPresentor presentor;

    public abstract bool TryAddEffects(List<EffectSettings> effects, Team team);

    public abstract void Death();
    public virtual UnitPresentor GetPresentor()
    {
        return presentor;
    }

    public virtual Team GetTeam()
    {
        return presentor.Team;
    }
}
