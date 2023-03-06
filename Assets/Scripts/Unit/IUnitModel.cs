using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.AI;
using UnityEngine;

namespace Unit
{
    public interface IUnitModel
    {
        Transform Transform { get; }
        IMovement Movement { get; }
        IHealthPresentor Health { get; }
        IWeapon Weapon { get; }

        List<IEffectPresentor> Effects { get; }

        Vector3 Position { get; }
        NavMeshAgent Agent { get; }
    }
}
