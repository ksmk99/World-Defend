using Zenject;

public class PlayerView : UnitView
{

    [Inject]
    public void Init(PlayerPresenter presenter)
    {
        this.presenter = presenter;
    }

    public override void Death()
    {
        return;
    }

    public override int GetPoolID()
    {
        return 0;
    }

    public class Factory : PlaceholderFactory<UnityEngine.Object, PlayerView>
    {
    }
}
