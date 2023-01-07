using System;
using UnityEngine;

public class InputService
{
    private Joystick joystick;

    public InputService(Joystick joystick)
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
}