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
}
