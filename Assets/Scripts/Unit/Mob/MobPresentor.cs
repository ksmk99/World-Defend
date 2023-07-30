using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit;
using UnityEngine;

public class MobPresentor : UnitPresentor
{
    private MobModel mobModel;

    public MobPresentor(MobModel model)
    {
        this.model = model; 
        mobModel = model;
    }

    public void SetPlayer(UnitPresentor presentor)
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
