using System.Collections.Generic;
using Unit;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class MobModel : IUnitModel
{
    public UnitPresentor Target { get; set; }
    public Transform Transform { get; }
    public IMovement Movement { get; }
    public IHealthPresentor Health { get; }
    public IWeaponPresentor Weapon { get; }
    public List<IEffectPresentor> Effects { get; }

    public Vector3 Position => Transform.position;
    public NavMeshAgent Agent { get; }

    public Team Team => Team.Ally;

    public bool IsActive { get; set; }

    public MobModel(
        IWeaponPresentor weapon,
        MobView view,
        IMovement movement,
        IHealthPresentor health)
    {
        Movement = movement;
        Health = health;
        Weapon = weapon;

        Effects = new List<IEffectPresentor>();
        Agent = view.transform.GetComponent<NavMeshAgent>();
        Transform = view.transform;
        IsActive = false;
    }
}

