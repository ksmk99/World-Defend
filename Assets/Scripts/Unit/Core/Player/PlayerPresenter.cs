using Helpers;
using Unit;
using Unity.Barracuda;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPresenter : UnitPresenter
{
    public PlayerPresenter(IUnitModel model)
    {
        this.model = model;

        model.Health.OnDeath += Death;
        model.Health.OnDamage += () => model.SignalBus.TryFire(new SignalOnDamage(this, model.Team));
    }

    public override void Death()
    {
        base.Death();

        if (!model.IsActive)
        {
            return;
        }

        model.IsActive = false;
        model.SignalBus.TryFire(new SignalOnPlayerDeath(model.RoomIndex, model.UnitView));
    }

    public override void Reset(SignalOnRoomResetUnits signal)
    {
        base.Reset(signal);

        if (!model.IsActive)
        {
            return;
        }


        model.IsActive = false;
        model.SignalBus.TryFire(new SignalOnPlayerReset(model.RoomIndex, model.UnitView));
    }

    public override void Respawn()
    {
        base.Respawn();

        model.IsActive = true;
    }
}