using UnityEngine;

public interface IInputService
{
    void SetMoveDirection(Vector3 direction);
    Vector3 GetMoveDirection();
}