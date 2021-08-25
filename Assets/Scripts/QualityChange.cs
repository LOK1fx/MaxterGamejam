using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace LOK1game
{
    public class QualityChange : MonoBehaviour
    {
        private TMP_Dropdown _dropdown;

        private void Start()
        {
            _dropdown = GetComponent<TMP_Dropdown>();
        }

        public void SetQuality(int index)
        {
            QualitySettings.SetQualityLevel(index);
        }
    }
}