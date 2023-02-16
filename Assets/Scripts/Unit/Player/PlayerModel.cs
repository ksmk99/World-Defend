
using System.Collections.Generic;
using Unit;
using UnityEngine;

public class PlayerModel
{
    public Transform Transform { get; }
    public IMovement Movement { get; }
    public IHealthPresentor Health { get; }
    public IWeapon Weapon { get; }

    public List<IEffectPresentor> Effects { get; }

    public Vector3 Position => Transform.position;

    public PlayerModel(Transform transform, IMovement movement,
        IHealthPresentor health, IWeapon weapon)
    {
        Transform = transform;
        Movement = movement;
        Health = health;
        Weapon = weapon;

        Effects = new List<IEffectPresentor>();
    }
}

