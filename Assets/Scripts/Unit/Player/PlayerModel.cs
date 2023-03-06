
using System.Collections.Generic;
using Unit;
using UnityEngine;
using UnityEngine.AI;

public class PlayerModel : IUnitModel
{
    public Transform Transform { get; }
    public IMovement Movement { get; }
    public IHealthPresentor Health { get; }
    public IWeapon Weapon { get; }

    public List<IEffectPresentor> Effects { get; }

    public Vector3 Position => Transform.position;
    public NavMeshAgent Agent { get; }

    public PlayerModel(IMovement movement,
        IHealthPresentor health, IWeapon weapon, Transform transform)
    {
        Transform = transform;
        Movement = movement;
        Health = health;
        Weapon = weapon;

        Effects = new List<IEffectPresentor>();
        Agent = transform.GetComponent<NavMeshAgent>();
    }
}

