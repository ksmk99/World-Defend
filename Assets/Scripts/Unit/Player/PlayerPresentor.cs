using System;
using System.Collections.Generic;
using Unit;
using UnityEngine;
using Zenject;

public class PlayerPresentor : UnitPresentor
{
    public PlayerPresentor(IUnitModel model)
    {
        this.model = model;
    }
}
