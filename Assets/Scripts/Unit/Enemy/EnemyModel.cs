using System.Collections.Generic;
using Unit;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class EnemyModel : IUnitModel
{
    public Transform Transform { get; }
    public IMovement Movement { get; }
    public IHealthPresentor Health { get; }
    public IWeaponPresentor Weapon { get; }
    public List<IEffectPresentor> Effects { get; }

    public Vector3 Position => Transform.position;
    public NavMeshAgent Agent { get; }

    public Team Team => Team.Enemy;

    public bool IsActive { get; set; }

    public EnemyModel(
        IWeaponPresentor weapon,
        Transform transform,
        IMovement movement,
        IHealthPresentor health)
    {
        Movement = movement;
        Health = health;
        Weapon = weapon;

        Effects = new List<IEffectPresentor>();
        Agent = transform.GetComponent<NavMeshAgent>();
        Transform = transform;
        IsActive = true;
    }
}
