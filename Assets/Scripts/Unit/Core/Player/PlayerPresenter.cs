using Helpers;
using Unit;
using Unity.Barracuda;
using UnityEngine;

public class PlayerPresenter : UnitPresenter
{
    public PlayerPresenter(IUnitModel model)
    {
        this.model = model;

        model.Health.OnDeath += OnDeath;
    }

    public override void OnDeath()
    {
        model.Health.OnDeath -= OnDeath;

        model.UnitView.Death();
        model.SignalBus.TryFire<SignalOnPlayerDeath>();
    }
}