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

    public EnemyModel(//[Inject(Id = "EW")] 
        IWeaponPresentor weapon,
        EnemyView view,
        IMovement movement,
        IHealthPresentor health)
    {
        Movement = movement;
        Health = health;
        Weapon = weapon;

        Effects = new List<IEffectPresentor>();
        Agent = view.transform.GetComponent<NavMeshAgent>();
        Transform = view.transform;
    }
}
