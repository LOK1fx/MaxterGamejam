using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game
{
    public static class Settings
    {
        public const string SENSIVITY = "Sensivity";

        public static float GetSensivity()
        {
            var sens = PlayerPrefs.GetFloat(SENSIVITY);

            if (sens == 0)
            {
                return 5f;
            }
            else
            {
                return sens;
            }
        }

        public static void SetSensivity(float value)
        {
            PlayerPrefs.SetFloat(SENSIVITY, value);
        }
    }
}