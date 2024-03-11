using Helpers;
using UnityEngine;

public class EnemyPresenter : UnitPresenter
{
    public EnemyPresenter(EnemyModel model, EnemyView view)
    {
        this.model = model;

        model.Health.OnDeath += Death;
        model.Health.OnDamage += () => model.SignalBus.TryFire(new SignalOnDamage(this, model.Team));
    }

    public override void Death()
    {
        base.Death();

        if (model.IsActive)
        {
            model.IsActive = false;
            model.SignalBus.TryFire(new SignalOnEnemyDeath(model.RoomIndex, model.UnitView));
        }
    }

    public override void Reset(SignalOnRoomResetUnits signal)
    {
        base.Reset(signal);

        if (model.Health.IsDead())
        {
            return;
        }

        model.IsActive = false;
        model.SignalBus.TryFire(new SignalOnEnemyReset(model.RoomIndex, model.UnitView));
    }

    public override void SetPlayer(UnitPresenter presenter)
    {
        model.Movement.SetTarget(presenter);
        model.IsActive = true;
    }
}
