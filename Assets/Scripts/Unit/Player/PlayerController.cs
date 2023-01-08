using Unit;
using Zenject;

public class PlayerPresentor : ITickable
{
    private readonly IMovement movement;
    private readonly IHealth health;
    private readonly IWeapon weapon;

    private readonly HealthModel healthModel;

    public PlayerPresentor(IMovement movement, IHealth health, 
        HealthModel healthModel, IWeapon weapon)
    {
        this.movement = movement;
        this.health = health;
        this.healthModel = healthModel;
        this.weapon = weapon;
    }

    public void Tick()
    {
        if(healthModel.IsDeath)
        {
            return;
        }

        movement.Move();
        health.AutoHeal();
        weapon.Update();
    }
}
