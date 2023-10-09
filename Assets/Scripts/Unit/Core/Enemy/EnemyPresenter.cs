using Helpers;
using Unity.VisualScripting;

public class EnemyPresenter : UnitPresenter
{
    public EnemyPresenter(EnemyModel model, EnemyView view)
    {
        this.model = model;
        //this.view = view;

        view.OnRespawn += Respawn;
        view.OnTryAddEffects += AddEffects;
        view.OnPresenterCall += () => this;
    }

    public override void OnDeath()
    {
        if (model.Health.IsDead())
        {
            Transform.GetComponent<UnitView>().Death();
            model.SignalBus.TryFire<SignalOnEnemyDeath>();
        }
    }
}
