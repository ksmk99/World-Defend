using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private Slider timeSlider;
        [Space]
        [SerializeField] private Image timeImage;
        [SerializeField] private Gradient timeGradient;

        public void SetValue(float value)
        {
            timeSlider.value = value;
            timeImage.color = timeGradient.Evaluate(value);
        }
    }
}