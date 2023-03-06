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
        if(model.Health.IsDeath())
        {
            return;
        }

        foreach (var effect in model.Effects)
        {
            effect.Update();
        }

        model.Movement.Move();
        model.Health.AutoHeal();
        model.Weapon.Update();
    }

    public void AddEffects(List<IEffectSettings> effects, Team team)
    {
        foreach (var effect in effects)
        {
            var effectModel = new EffectModel(effect);
            var presentor = effectModel.Settings.GetPresentor(model, effectModel);
            model.Effects.Add(presentor);
            presentor.OnEffectEnd += (x) => model.Effects.Remove(x);
        }
    }
}
