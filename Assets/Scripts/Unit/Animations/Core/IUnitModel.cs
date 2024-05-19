using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Unit
{
    public interface IUnitModel
    {
        int RoomIndex { get; set; }
        Team Team { get; }
        Transform Transform { get; }
        UnitView UnitView { get; }
        bool IsActive { get; set; }
        Vector3 Position { get; }

        IMovement Movement { get; }
        IHealthPresenter Health { get; }
        IWeaponPresenter Weapon { get; }

        List<IEffectPresenter> Effects { get; }

        SignalBus SignalBus { get; }
        bool IsDeathFired { get; set; }
    }
}
