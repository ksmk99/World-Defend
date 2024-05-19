
using UnityEngine;

public interface IMovement
{
    void Move(bool isDead, Transform target = null);
    void SetTarget(UnitPresenter presenter);
}
