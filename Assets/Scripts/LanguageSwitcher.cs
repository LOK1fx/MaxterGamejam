using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace LOK1game
{
    public class LanguageSwitcher : MonoBehaviour
    {
        private TMP_Dropdown _dropdown;

        private void Start()
        {
            _dropdown = GetComponent<TMP_Dropdown>();
        }

        public void OnValueChanged(int index)
        {
            LocalisationSystem.language = (LocalisationSystem.Language)index;
        }
    }
}