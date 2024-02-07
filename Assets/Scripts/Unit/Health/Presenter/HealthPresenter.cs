﻿using Helpers;
using System;
using UnityEngine;
using Zenject;
using static UnityEditor.Profiling.HierarchyFrameDataView;

namespace Unit
{
    public class HealthPresenter : IHealthPresenter, IInitializable, ITickable
    {
        private readonly SignalBus signalBus;
        private readonly HealthView view;
        private readonly HealthModel model;

        public event Action OnDeath;

        public HealthPresenter(HealthView view, HealthModel model, SignalBus signalBus)
        {
            this.view = view;
            this.model = model;
            this.signalBus = signalBus;
        }

        public void Initialize()
        {
            model.Follower.Init(view);
            HealthSignal();
        }

        public void Tick()
        {
            if(model.IsDead)
            {
                return;
            }

            model.Follower.Update(view, model.IsDead);
            AutoHeal();
        }

        public bool IsDead()
        {
            return model.Health <= 0;
        }

        public void Damage(int count)
        {
            if (IsDead())
            {
                return;
            }

            model.Health -= count;
            model.Health = Math.Clamp(model.Health, 0, model.Settings.MaxHealth);

            var signal = new SignalOnUnitDamage(model.Health / model.Settings.MaxHealth, model.Health);
            signalBus.TryFire(signal);

            model.NextHealTime = Time.time + model.Settings.HealDelay;

            CheckDeath();
        }

        private void CheckDeath()
        {
            if (model.Health <= 0)
            {
                model.IsDead = true;
                model.Follower.Disable(view);
                OnDeath?.Invoke();
            }
        }

        public void Heal(int count)
        {
            model.Health += count;
            model.Health = Math.Clamp(model.Health, 0, model.Settings.MaxHealth);

            if (model.Health > 0)
            {
                model.IsDead = false;
            }

            HealthSignal();
        }

        public void Reset()
        {
            Heal(int.MaxValue);
            view.UpdateValues(new SignalOnUnitHeal(1, model.Health));
        }

        private void HealthSignal()
        {
            var signal = new SignalOnUnitHeal(model.Health / model.Settings.MaxHealth, model.Health);
            signalBus.TryFire(signal);
        }

        private void AutoHeal()
        {
            if (!model.Settings.IsAutoHeal || model.Health.Equals(model.Settings.MaxHealth) || IsDead())
            {
                return;
            }

            if (Time.time >= model.NextHealTime)
            {
                Heal(model.Settings.HealCount);
                model.NextHealTime = Time.time + model.Settings.HealRate;
            }
        }
    }
}
