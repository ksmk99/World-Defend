using Helpers;
using Unit;

public class PlayerPresenter : UnitPresenter
{
    public PlayerPresenter(IUnitModel model)
    {
        this.model = model;
    }

    public override void OnDeath()
    {
        if (model.Health.IsDead())
        {
            Transform.GetComponent<UnitView>().Death();
            model.SignalBus.TryFire<SignalOnPlayerDeath>();
        }
    }
}
