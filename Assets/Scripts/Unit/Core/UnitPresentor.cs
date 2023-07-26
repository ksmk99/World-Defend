using System.Collections.Generic;
using Unit;
using UnityEngine;
using Zenject;

public abstract class UnitPresentor : ITickable
{
    public Transform Transform => model.Transform;
    public Team Team => model.Team;

    protected IUnitModel model;

    public virtual void Tick()
    {
        var isDead = model.Health.IsDeath();
        model.Health.AutoHeal();
        model.Weapon.Update(model.Transform, isDead, model.Team);
        model.Movement.Move(isDead);

        for (var i = 0; i < model.Effects.Count; i++)
        {
            model.Effects[i].Update();
        }
    }

    public void OnDeath()
    {
        if (model.Health.IsDeath())
        {
            Transform.GetComponent<UnitView>().Death();
        }
    }

    public void Respawn()
    {
        model.Health.Heal(int.MaxValue);
        model.Weapon.Reset();
    }

    public virtual bool AddEffects(List<EffectSettings> effects, Team team)
    {
        if (model.Team == team)
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
