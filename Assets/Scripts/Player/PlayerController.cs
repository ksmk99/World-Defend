using Zenject;

public class PlayerController : ITickable
{
    private IMovement movement;

    public PlayerController(IMovement movement)
    {
        this.movement = movement;
    }

    public void Tick()
    {
        movement.Move();
    }
}
