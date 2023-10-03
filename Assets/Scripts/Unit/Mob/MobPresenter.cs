﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit;
using UnityEngine;

public class MobPresenter : UnitPresenter
{
    private MobModel mobModel;

    public MobPresenter(MobModel model)
    {
        this.model = model; 
        mobModel = model;
    }

    public void SetPlayer(UnitPresenter presentor)
    {
        mobModel.Target = presentor;
        mobModel.IsActive = true;
    }

    public Transform GetTarget()
    {
        if(mobModel.Target == null)
        {
            throw new Exception("Target is not set");
        }

        return mobModel.Target.Transform;
    }
}
