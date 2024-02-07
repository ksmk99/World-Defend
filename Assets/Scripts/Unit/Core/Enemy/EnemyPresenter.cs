using Helpers;
using Unity.VisualScripting;

public class EnemyPresenter : UnitPresenter
{
    public EnemyPresenter(EnemyModel model, EnemyView view)
    {
        this.model = model;

        view.OnRespawn += Respawn;

        model.Health.OnDeath += OnDeath; 
    }

    public override void OnDeath()
    {
        model.SignalBus.TryFire(new SignalOnEnemyDeath(model.RoomIndex));
        Transform.GetComponent<UnitView>().Death();
    }

    public override void SetPlayer(UnitPresenter presenter)
    {
        model.Movement.SetTarget(presenter);
        model.IsActive = true;
    }
}
