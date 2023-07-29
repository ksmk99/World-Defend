using System.Collections.Generic;
using Unit;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class MobModel : PlayerModel
{
    public MobModel(IMovement movement, IHealthPresentor health, IWeaponPresentor weapon, Transform transform) 
        : base(movement, health, weapon, transform, false) { }
}

