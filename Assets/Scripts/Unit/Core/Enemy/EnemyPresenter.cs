using Helpers;
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
        model.SignalBus.TryFire(new SignalOnEnemyDeath(model.RoomIndex, model.UnitView));
        model.UnitView.Death();
    }

    public override void Reset(SignalOnRoomReset signal)
    {
        model.SignalBus.TryFire(new SignalOnEnemyReset(model.RoomIndex, model.UnitView));
    }

    public override void SetPlayer(UnitPresenter presenter)
    {
        model.Movement.SetTarget(presenter);
        model.IsActive = true;
    }
}
