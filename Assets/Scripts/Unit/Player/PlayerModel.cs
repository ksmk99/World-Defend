
using System.Collections.Generic;
using Unit;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class PlayerModel : IUnitModel
{
    public Transform Transform { get; }
    public bool IsActive { get; set; }
    public IMovement Movement { get; }
    public IHealthPresentor Health { get; }
    public IWeaponPresentor Weapon { get; }

    public List<IEffectPresentor> Effects { get; }

    public Vector3 Position => Transform.position;
    public NavMeshAgent Agent { get; }
    public Team Team => Team.Ally;

    public PlayerModel(IMovement movement, IHealthPresentor health, IWeaponPresentor weapon, Transform transform, bool isActive = true)
    {
        Transform = transform;
        Movement = movement;
        Health = health;
        Weapon = weapon;
        IsActive = isActive;

        Effects = new List<IEffectPresentor>();
        Agent = transform.GetComponent<NavMeshAgent>();
    }
}

