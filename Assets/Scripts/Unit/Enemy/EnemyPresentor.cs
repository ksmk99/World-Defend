using Zenject;

public class EnemyPresentor : ITickable
{
    private readonly EnemyModel model;

    public EnemyPresentor(EnemyModel model)
    {
        this.model = model;
    }

    public void Tick()
    {
        model.Movement.Move();
    }
}
