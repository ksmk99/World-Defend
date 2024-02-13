using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit;
using UnityEngine;

public class MobPresenter : UnitPresenter
{
    private MobModel mobModel;

    public MobPresenter(MobModel model)
    {
        this.model = model; 
        mobModel = model;

        model.Health.OnDeath += Death;
    }

    public override void Death()
    {
        model.Health.OnDeath -= Death;

        Transform.GetComponent<UnitView>().Death();
        model.SignalBus.TryFire(new SignalOnMobDeath(model.RoomIndex, model.UnitView));
    }

    public override void Reset(SignalOnRoomReset signal)
    {
        model.SignalBus.TryFire(new SignalOnMobReset(model.RoomIndex, model.UnitView));
    }

    public override void SetPlayer(UnitPresenter presenter)
    {
        mobModel.Target = presenter;
        mobModel.Movement.SetTarget(presenter);
        mobModel.IsActive = true;
    }

    public Transform GetTarget()
    {
        if(mobModel.Target == null)
        {
            throw new Exception("Target is not set");
        }

        return mobModel.Target.Transform;
    }
}
