using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class LevelProgressView : MonoBehaviour, ILevelProgressView
    {
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private Scrollbar scrollbar;

        public void Init(int level)
        {
            levelText.text = level.ToString();
            scrollbar.value = 0;
        }

        public void SetValue(float value)
        {
            scrollbar.value = value;
        }
    }
}
