
using System.Collections.Generic;
using Unit;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class PlayerModel : IUnitModel
{
    public int RoomIndex { get; set; }

    public Transform Transform { get; }
    public bool IsActive { get; set; }
    public IMovement Movement { get; }
    public IHealthPresenter Health { get; }
    public IWeaponPresenter Weapon { get; }

    public List<IEffectPresenter> Effects { get; }

    public Vector3 Position => Transform.position;
    public Team Team => Team.Ally;

    public SignalBus SignalBus { get; }
    public bool IsDeathFired { get; set; }

    public UnitView UnitView { get; }

    public PlayerModel(
        IMovement movement, 
        IHealthPresenter health, 
        IWeaponPresenter weapon, 
        SignalBus signalBus,
        Transform transform)
    {
        Transform = transform;
        Movement = movement;
        Health = health;
        Weapon = weapon;
        SignalBus = signalBus;
        IsActive = true;

        Effects = new List<IEffectPresenter>();
        UnitView = Transform.GetComponent<UnitView>();
    }
}

