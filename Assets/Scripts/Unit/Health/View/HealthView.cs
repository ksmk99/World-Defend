using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Unit
{
    public class HealthView : MonoBehaviour, IInitializable
    {
        [SerializeField] private Image healthBar;

        private SignalBus signalBus;

        [Inject]
        public void Init(SignalBus signalBus)
        {
            this.signalBus = signalBus;
        }

        public void Initialize()
        {
            signalBus.Subscribe<SignalOnUnitHeal>(Heal);
            signalBus.Subscribe<SignalOnUnitDamage>(Damage);
        }

        private void OnDestroy()
        {
            signalBus.Unsubscribe<SignalOnUnitHeal>(Heal);
            signalBus.Unsubscribe<SignalOnUnitDamage>(Damage);
        }

        private void Heal(SignalOnUnitHeal data)
        {   
            healthBar.fillAmount = data.Percent;
        }

        private void Damage(SignalOnUnitDamage data)
        {
            healthBar.fillAmount = data.Percent;
        }
    }
}
