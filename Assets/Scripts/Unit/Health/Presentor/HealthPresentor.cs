using System;
using UnityEngine;
using Zenject;

namespace Unit
{

    #region Signals
    public class SignalOnUnitDied { }

    public interface ISignalOnHealthChange
    {
        public float Percent { get; }
    }

    public class SignalOnUnitDamage : ISignalOnHealthChange
    {
        public float Percent { get; }

        public SignalOnUnitDamage(float percent)
        {
            Percent = percent;  
        }
    }

    public class SignalOnUnitHeal : ISignalOnHealthChange
    {
        public float Percent { get; }

        public SignalOnUnitHeal(float percent)
        {
            Percent = percent;
        }
    }
    #endregion

    public class HealthPresentor : IHealthPresentor
    {
        private readonly SignalBus signalBus;
        private readonly HealthModel model;

        public HealthPresentor(HealthModel model, SignalBus signalBus)
        {
            this.model = model;
            this.signalBus = signalBus;
        }

        public bool IsDeath()
        {
            return model.Health <= 0; 
        }

        public void Damage(int count)
        {
            model.Health -= count;
            model.Health = Math.Clamp(model.Health, 0, model.Settings.MaxHealth);

            if (model.Health == 0)
            {
                model.IsDeath = true;
                signalBus.TryFire<SignalOnUnitDied>();
            }
            else
            {
                var signal = new SignalOnUnitDamage(model.Health / model.Settings.MaxHealth);
                signalBus.TryFire(signal);

                model.NextHealTime = Time.time + model.Settings.HealDelay;
            }
        }

        public void Heal(int count)
        {
            model.Health += count;
            model.Health = Math.Clamp(model.Health, 0, model.Settings.MaxHealth);

            var signal = new SignalOnUnitHeal(model.Health / model.Settings.MaxHealth);
            signalBus.TryFire(signal);
        }

        public void AutoHeal()
        {
            if (!model.Settings.IsAutoHeal || model.Health.Equals(model.Settings.MaxHealth))
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
