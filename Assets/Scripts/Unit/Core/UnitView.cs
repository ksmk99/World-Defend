using System.Collections.Generic;
using Unit;
using UnityEngine;

public abstract class UnitView : MonoBehaviour
{

    public abstract bool TryAddEffects(List<EffectSettings> effects, Team team);

    public abstract void Death();
}
