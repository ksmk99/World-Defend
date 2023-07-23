using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Unit
{
    public interface IUnitModel
    {
        Transform Transform { get; }
        IMovement Movement { get; }
        IHealthPresentor Health { get; }
        IWeaponPresentor Weapon { get; }

        List<IEffectPresentor> Effects { get; }

        Vector3 Position { get; }
        NavMeshAgent Agent { get; }
    }
}
