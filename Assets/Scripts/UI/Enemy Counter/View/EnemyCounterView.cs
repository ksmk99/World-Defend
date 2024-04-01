using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class EnemyCounterView : MonoBehaviour
    {
        [SerializeField] private TMP_Text countText;
        [SerializeField] private Animation animation;

        public void SetValue(int count)
        {
            countText.text = count.ToString();
            animation.Play();
        }
    }
}