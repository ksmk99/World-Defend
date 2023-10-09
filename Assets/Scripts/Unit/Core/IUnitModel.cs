using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Unit
{
    public interface IUnitModel
    {
        Team Team { get; }
        Transform Transform { get; }
        bool IsActive { get; set; }
        Vector3 Position { get; }

        IMovement Movement { get; }
        IHealthPresenter Health { get; }
        IWeaponPresenter Weapon { get; }

        List<IEffectPresenter> Effects { get; }

        SignalBus SignalBus { get; }
    }
}
