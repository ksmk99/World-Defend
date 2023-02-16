using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit;
using UnityEngine;
using Zenject;

public class PlayerView : MonoBehaviour
{
    private PlayerPresentor presentor;

    [Inject]
    public void Init(PlayerPresentor presentor)
    {
        this.presentor = presentor;
    }

    public void AddEffects(List<IEffectSettings> effects, Team team)
    {
        presentor.AddEffects(effects, team);
    }
}
