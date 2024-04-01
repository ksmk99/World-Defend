using Cysharp.Threading.Tasks;
using Helpers;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Unit
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Image healthBar;
        [Space]
        [SerializeField] private Image damageBar;
        [SerializeField] private float damageChangeSpeed;
        [SerializeField] private AnimationCurve damageCurve;
        [Space]
        [SerializeField] private RectTransform rect;
        [SerializeField] private TMP_Text healthText;
        [SerializeField] private Vector3 offset;
        [Space]
        [SerializeField] private Animation animation;
        [SerializeField] private TMP_Text nicknameText;

        public RectTransform Rect => rect;
        public Vector3 Offset => offset;

        private SignalBus signalBus;
        private CancellationTokenSource cts;

        [Inject]
        public void Init(SignalBus signalBus, Sprite sprite, string name)
        {
            this.signalBus = signalBus;
            healthBar.sprite = sprite;
            nicknameText.text = name;   

            cts = new CancellationTokenSource();
        }

        private void OnEnable()
        {
            signalBus.Subscribe<SignalOnUnitHeal>(HealReact);
            signalBus.Subscribe<SignalOnUnitDamage>(DamageReact);
        }

        private void OnDisable()
        {
            signalBus.Unsubscribe<SignalOnUnitHeal>(HealReact);
            signalBus.Unsubscribe<SignalOnUnitDamage>(DamageReact);
        }

        public void HealReact(ISignalOnHealthChange data)
        {
            healthBar.fillAmount = data.Health / data.MaxHealth;
            healthText.text = data.Health.ToString();
        }

        public void DamageReact(ISignalOnHealthChange data)
        {
            healthBar.fillAmount = data.Health / data.MaxHealth;
            healthText.text = data.Health.ToString();
            animation.Play();

            cts.Cancel();
            cts = new CancellationTokenSource();
            MoveDamageZone(data.Health / data.MaxHealth, cts);
        }

        private async void MoveDamageZone(float value, CancellationTokenSource cts)
        {
            float startValue = damageBar.fillAmount;
            var t = 0f;
            while (damageBar.fillAmount > value)
            {
                if (cts.IsCancellationRequested || damageBar == null)
                {
                    break;
                }

                t += Mathf.Abs(startValue - value) * damageChangeSpeed * Time.deltaTime;
                damageBar.fillAmount = Mathf.Lerp(startValue, value, damageCurve.Evaluate(t));

                await UniTask.DelayFrame(1);
            }
        }

        public class Factory : PlaceholderFactory<HealthView>
        {

        }
    }
}
