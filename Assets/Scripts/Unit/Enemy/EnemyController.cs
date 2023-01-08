using Zenject;

public class EnemyPresentor : ITickable
{
    private IMovement movement;

    public EnemyPresentor(IMovement movement)
    {
        this.movement = movement;
    }

    public void Tick()
    {
        movement.Move();
    }
}
