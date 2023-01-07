using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Unit
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Image healthBar;

        private SignalBus signalBus;

        [Inject]
        public void Init(SignalBus signalBus)
        {
            this.signalBus = signalBus;

            signalBus.Subscribe<ISignalOnHealthChange>(x => SetHealthValue(x.Percent));
        }

        public void SetHealthValue(float percent)
        {
            healthBar.fillAmount = percent;
        }
    }
}
