using System.Collections.Generic;
using Unit;
using UnityEngine;
using Zenject;

public class MobModel : IUnitModel
{
    public int RoomIndex { get; set; }
    public UnitPresenter Target { get; set; }
    public Transform Transform { get; }
    public IMovement Movement { get; }
    public IHealthPresenter Health { get; }
    public IWeaponPresenter Weapon { get; }
    public List<IEffectPresenter> Effects { get; }

    public SignalBus SignalBus { get; }

    public Vector3 Position => Transform.position;
    public Team Team => Team.Ally;

    public bool IsActive { get; set; }
    public bool IsDeathFired { get; set; }

    public UnitView UnitView
    {
        get;
    }

    public MobModel(
        IWeaponPresenter weapon,
        Transform transform,
        IMovement movement,
        IHealthPresenter health,
        SignalBus signalBus)
    {
        Movement = movement;
        Health = health;
        Weapon = weapon;

        Transform = transform;
        SignalBus = signalBus;

        Effects = new List<IEffectPresenter>();
        UnitView = Transform.GetComponent<UnitView>();
    }
}

