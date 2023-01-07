using Zenject;

public class EnemyController : ITickable
{
    private IMovement movement;

    public EnemyController(IMovement movement)
    {
        this.movement = movement;
    }

    public void Tick()
    {
        movement.Move();
    }
}
