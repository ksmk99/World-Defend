
using Unit;
using UnityEngine;

public class PlayerModel
{
    public Transform Transform { get; private set; }
    public Vector3 Position => Transform.position;

    public PlayerModel(Transform transform)
    {
        this.Transform = transform;
    }
}

