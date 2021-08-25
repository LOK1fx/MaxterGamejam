using UnityEngine;
using TMPro;

namespace com.LOK1game.recode.UI
{
    public class AccelerateText : MonoBehaviour
    {
        [SerializeField] private PlayerMoveData _data;

        private TMP_Text _text;

        private void Start()
        {
            _text = GetComponent<TMP_Text>();
        }

        private void Update()
        {
            _text.text = $"Accelerate: {_data.airAccelerate}";
        }
    }
}