using UnityEngine;
using System.Collections;
using TMPro;

namespace com.LOK1game.recode
{
    public class Subtitle : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void SetText(string key, string speakerKey = "")
        {
            if(speakerKey == "")
            {
                _text.text = LocalisationSystem.GetLocalisedValue(key);
            }
            else
            {
                _text.text = $"<b>{LocalisationSystem.GetLocalisedValue(speakerKey)}</b>: <alpha=#CC>{LocalisationSystem.GetLocalisedValue(key)}";
            }
        }
    }
}
