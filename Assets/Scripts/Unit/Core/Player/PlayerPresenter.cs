using Helpers;
using Unit;
using Unity.Barracuda;
using UnityEngine;

public class PlayerPresenter : UnitPresenter
{
    public PlayerPresenter(IUnitModel model)
    {
        this.model = model;

        model.Health.OnDeath += Death;
    }

    public override void Death()
    {
        model.SignalBus.TryFire(new SignalOnPlayerDeath(model.RoomIndex, model.UnitView));
        model.UnitView.Death();
    }

    public override void Reset(SignalOnRoomReset signal)
    {
        if (model.Health.IsDead())
        {
            return;
        }
     
        model.SignalBus.TryFire(new SignalOnPlayerReset(model.RoomIndex, model.UnitView));
    }
}