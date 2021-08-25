using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LOK1game
{
    [RequireComponent(typeof(Image))]
    public class MenuPanel : MonoBehaviour
    {
        public enum ColorType
        {
            Primary,
            Secondary
        }

        [SerializeField] private MenuAssets _assets;

        [Space]
        [SerializeField] private ColorType _color;

        private Image _image;

        private void OnValidate()
        {
            _image = GetComponent<Image>();
        }

        [ContextMenu("Change Color")]
        public void ChangeColor()
        {
            switch (_color)
            {
                case ColorType.Primary:
                    _image.color = _assets.PrimaryColor;
                    break;
                case ColorType.Secondary:
                    _image.color = _assets.SecondaryColor;
                    break;
                default:
                    _image.color = Color.white;
                    break;
            }
        }
    }
}