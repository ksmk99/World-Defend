using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit;
using UnityEngine;
using Zenject;

public abstract class UnitView : MonoBehaviour
{

    public abstract bool TryAddEffects(List<EffectSettings> effects, Team team);

    public abstract void Death();
}
