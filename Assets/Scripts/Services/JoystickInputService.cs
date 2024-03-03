using UnityEngine;

public class JoystickInputService : IInputService
{
    private Joystick joystick;

    public JoystickInputService(Joystick joystick)
    {
        this.joystick = joystick;
    }


    public Vector3 GetMoveDirection()
    {
        float vertical = joystick.Direction.y; //Input.GetAxis("Vertical");
        float horizontal = joystick.Direction.x; //Input.GetAxis("Horizontal");

        Vector3 direction = new Vector3(horizontal, 0, vertical);

        return direction;
    }

    public void SetMoveDirection(Vector3 direction)
    {
        return;
    }
}