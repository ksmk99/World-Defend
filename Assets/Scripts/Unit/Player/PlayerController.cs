using Unit;
using Zenject;

public class PlayerController : ITickable
{
    private IMovement movement;
    private IHealth health;

    public PlayerController(IMovement movement, IHealth health)
    {
        this.movement = movement;
        this.health = health;
    }

    public void Tick()
    {
        movement.Move();
        health.AutoHeal();
    }
}
