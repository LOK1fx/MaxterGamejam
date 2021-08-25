using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cam.lok1game.KekFire
{
    public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public string title;
        public string description;

        private void Start()
        {
            
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            TooltipSystem.Show(description, title);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TooltipSystem.Hide();
        }
    }
}