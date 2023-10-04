
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
    public IHealthPresenter Health { get; }
    public IWeaponPresenter Weapon { get; }

    public List<IEffectPresenter> Effects { get; }

    public Vector3 Position => Transform.position;
    public NavMeshAgent Agent { get; }
    public Team Team => Team.Ally;

    public PlayerModel(IMovement movement, IHealthPresenter health, IWeaponPresenter weapon, Transform transform, bool isActive = true)
    {
        Transform = transform;
        Movement = movement;
        Health = health;
        Weapon = weapon;
        IsActive = isActive;

        Effects = new List<IEffectPresenter>();
        Agent = transform.GetComponent<NavMeshAgent>();
    }
}

