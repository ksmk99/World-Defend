using System.Collections.Generic;
using Unit;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class EnemyModel : IUnitModel
{
    public Transform Transform { get; }
    public IMovement Movement { get; }
    public IHealthPresentor Health { get; }
    public IWeapon Weapon { get; }
    public List<IEffectPresentor> Effects { get; }

    public Vector3 Position => Transform.position;
    public NavMeshAgent Agent { get; }

    public EnemyModel(EnemyView view, IMovement movement, IHealthPresentor health)
    {
        Movement = movement;
        Effects = new List<IEffectPresentor>();
        Agent = view.transform.GetComponent<NavMeshAgent>();
        Transform = view.transform;
        Health = health;
    }
}
