using System;
using UnityEngine;
using Zenject;

namespace Unit
{

    #region Signals
    public class SignalOnUnitDied { }

    public interface ISignalOnHealthChange
    {
        public float Percent { get; set; }
    }

    public struct SignalOnUnitDamage : ISignalOnHealthChange
    {
        public float Percent { get; set; }
    }

    public struct SignalOnUnitHeal : ISignalOnHealthChange
    {
        public float Percent { get; set; }
    }
    #endregion

    public class HealthPresentor : IHealth
    {
        private readonly SignalBus signalBus;
        private readonly HealthModel model;

        public HealthPresentor(HealthModel model, SignalBus signalBus)
        {
            this.model = model;
            this.signalBus = signalBus;
        }

        public void Damage(int count)
        {
            model.Health -= count;
            model.Health = Math.Clamp(model.Health, 0, model.Settings.MaxHealth);

            if (model.Health == 0)
            {
                signalBus.Fire(new SignalOnUnitDied());
            }
            else
            {
                var signal = new SignalOnUnitDamage()
                {
                    Percent = model.Health / model.Settings.MaxHealth
                };
                signalBus.Fire(signal);

                Debug.Log("Damage - " + signal.Percent);
            }
        }

        public void Heal(int count)
        {
            model.Health += count;
            model.Health = Math.Clamp(model.Health, 0, model.Settings.MaxHealth);

            var signal = new SignalOnUnitHeal()
            {
                Percent = model.Health / model.Settings.MaxHealth
            };
            signalBus.Fire(signal);

            Debug.Log("Health count - " + signal.Percent);
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
