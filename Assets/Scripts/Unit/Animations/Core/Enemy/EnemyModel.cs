using System.Collections.Generic;
using Unit;
using UnityEngine;
using Zenject;

public class EnemyModel : IUnitModel
{
    public int RoomIndex { get; set; }
    public Transform Transform { get; }
    public IMovement Movement { get; }
    public IHealthPresenter Health { get; }
    public IWeaponPresenter Weapon { get; }
    public List<IEffectPresenter> Effects { get; }

    public SignalBus SignalBus { get; }

    public Vector3 Position => Transform.position;
    public Team Team => Team.Enemy;

    public bool IsActive { get; set; }
    public bool IsDeathFired { get; set; }

    public UnitView UnitView { get; }

    public EnemyModel(
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

        UnitView = Transform.GetComponent<UnitView>();
        Effects = new List<IEffectPresenter>();
        IsActive = false;
    }
}
