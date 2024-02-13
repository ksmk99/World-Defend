using Helpers;
using System;
using System.Collections.Generic;
using Unit;
using UnityEngine;
using Zenject;

public abstract class UnitPresenter : ITickable
{
    public Transform Transform => model.Transform;
    public Team Team => model.Team;
    public int RoomIndex => model.RoomIndex;    

    protected IUnitModel model;

    public virtual void Tick()
    {
        var isDead = model.Health.IsDead();
        if (isDead)
        {
            model.UnitView.Death();
            return;
        }

        for (var i = 0; i < model.Effects.Count; i++)
        {
            model.Effects[i].Update();
        }

        if (!model.IsActive)
        {
            return;
        }

        model.Weapon.Update(model.Transform, isDead, model.Team);
        model.Movement.Move(isDead, model.Weapon.GetTarget());
    }

    public abstract void Death();

    public void Respawn()
    {
        model.Health.Reset();
        model.Weapon.Reset();
    }

    public virtual bool AddEffects(List<EffectSettings> effects, Team team)
    {
        if (model.Team == team)
        {
            return false;
        }

        if(effects == null)
        {
            return false;
        }

        for (var i = 0; i < effects.Count; i++)
        {
            var effectModel = new EffectModel(effects[i]);
            var presenter = effectModel.Settings.GetPresenter(model, effectModel);
            model.Effects.Add(presenter);
            presenter.OnEffectEnd += (x) => model.Effects.Remove(x);
        }

        return true;
    }

    public virtual void SetPlayer(UnitPresenter playerPresenter)
    {

    }

    public void SetRoom(int roomIndex)
    {
        model.RoomIndex = roomIndex;
    }

    public abstract void Reset(SignalOnRoomReset signal);
}
