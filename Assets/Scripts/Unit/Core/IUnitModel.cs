using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Unit
{
    public interface IUnitModel
    {
        Transform Transform { get; }
        bool IsActive { get; set; }
        Team Team { get; }

        IMovement Movement { get; }
        IHealthPresenter Health { get; }
        IWeaponPresenter Weapon { get; }

        List<IEffectPresenter> Effects { get; }

        Vector3 Position { get; }
        NavMeshAgent Agent { get; }
    }
}
