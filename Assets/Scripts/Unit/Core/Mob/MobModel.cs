﻿using System.Collections.Generic;
using Unit;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class MobModel : IUnitModel
{
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

        Effects = new List<IEffectPresenter>();
        Transform = transform;
        IsActive = false;
        SignalBus = signalBus;
    }
}

