using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Unit
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Image healthBar;
        [SerializeField] private RectTransform rect;
        [SerializeField] private TMP_Text healthText;
        [SerializeField] private Vector3 offset;

        public RectTransform Rect => rect;
        public Vector3 Offset => offset;

        private SignalBus signalBus;

        [Inject]
        public void Init(SignalBus signalBus, Sprite barIcon)
        {
            this.signalBus = signalBus;
            healthBar.sprite = barIcon;
        }

        private void OnEnable()
        {
            signalBus.Subscribe<SignalOnUnitHeal>(UpdateValues);
            signalBus.Subscribe<SignalOnUnitDamage>(UpdateValues);
        }

        private void OnDisable()
        {
            signalBus.Unsubscribe<SignalOnUnitHeal>(UpdateValues);
            signalBus.Unsubscribe<SignalOnUnitDamage>(UpdateValues);
        }

        public void UpdateValues(ISignalOnHealthChange data)
        {
            healthBar.fillAmount = data.Percent;
            healthText.text = data.Health.ToString();
        }

        public class Factory : PlaceholderFactory<HealthView>
        {

        }
    }
}
