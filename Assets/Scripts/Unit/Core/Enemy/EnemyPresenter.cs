using Helpers;
using Unity.Barracuda;
using Unity.VisualScripting;

public class EnemyPresenter : UnitPresenter
{
    public EnemyPresenter(EnemyModel model, EnemyView view)
    {
        this.model = model;

        view.OnRespawn += Respawn;
        model.Health.OnDeath += Death;
    }

    public override void Death()
    {
        base.Death();

        model.IsActive = false;
        model.SignalBus.TryFire(new SignalOnEnemyDeath(model.RoomIndex, model.UnitView));
    }

    public override void Reset(SignalOnRoomReset signal)
    {
        base.Reset(signal);

        model.IsActive = false;
        model.SignalBus.TryFire(new SignalOnEnemyReset(model.RoomIndex, model.UnitView));

    }

    public override void SetPlayer(UnitPresenter presenter)
    {
        model.Movement.SetTarget(presenter);
        model.IsActive = true;
    }
}
