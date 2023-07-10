using System;
using System.Collections.Generic;
using Unit;
using UnityEngine;
using Zenject;

public class PlayerPresentor : ITickable
{
    private readonly IUnitModel model;

    public Transform Transform => model.Transform;

    public PlayerPresentor(IUnitModel model)
    {
        this.model = model;
    }

    public void Tick()
    {
        if (model.Health.IsDeath())
        {
            return;
        }

        for (var i = 0; i < model.Effects.Count; i++)
        {
            model.Effects[i].Update();
        }

        model.Movement.Move();
        model.Health.AutoHeal();
        model.Weapon.Update(model.Transform);
    }

    public bool AddEffects(List<EffectSettings> effects, Team team)
    {
        if(model.Weapon.GetTeam() == team)
        {
            return false;
        }

        for (var i = 0; i < effects.Count; i++)
        {
            var effectModel = new EffectModel(effects[i]);
            var presentor = effectModel.Settings.GetPresentor(model, effectModel);
            model.Effects.Add(presentor);
            presentor.OnEffectEnd += (x) => model.Effects.Remove(x);
        }

        return true;
    }
}
