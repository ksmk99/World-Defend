using System;
using System.Collections.Generic;
using Unit;
using UnityEngine;
using Zenject;

public class EnemyPresentor : UnitPresentor
{
    public EnemyPresentor(EnemyModel model)
    {
        this.model = model;
    }
}
