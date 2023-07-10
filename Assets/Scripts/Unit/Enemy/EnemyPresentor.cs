using System.Collections.Generic;
using Unit;
using UnityEngine;
using Zenject;

public class EnemyPresentor : ITickable
{
    private readonly EnemyModel model;

    public Transform Transform => model.Transform;

    public EnemyPresentor(EnemyModel model)
    {
        this.model = model;
    }

    public void Tick()
    {
        var isDead = model.Health.IsDeath();
        model.Health.AutoHeal(isDead);
        model.Weapon.Update(model.Transform, isDead);

        if (isDead)
        {
            return;
        }

        for (var i = 0; i < model.Effects.Count; i++)
        {
            model.Effects[i].Update();
        }

        model.Movement.Move();
    }

    public bool AddEffects(List<EffectSettings> effects, Team team)
    {
        if (model.Weapon.GetTeam() == team)
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
