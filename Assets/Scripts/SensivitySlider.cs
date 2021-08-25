using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LOK1game
{
    public class SensivitySlider : MonoBehaviour
    {
        private Slider _slider;

        private void Start()
        {
            _slider = GetComponent<Slider>();

            _slider.value = Settings.GetSensivity();
        }

        public void OnValueChanged()
        {
            Settings.SetSensivity(_slider.value);
        }
    }
}