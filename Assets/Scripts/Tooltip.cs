using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Cam.lok1game.KekFire
{
	public class Tooltip : MonoBehaviour
	{
        public TextMeshProUGUI headerField;
        public TextMeshProUGUI contentField;

        private RectTransform rectTransform;

	    private void Start()
	    {
            rectTransform = GetComponent<RectTransform>();
	    }

        public void SetText(string content, string header = "")
        {
            if(string.IsNullOrEmpty(header))
            {
                headerField.gameObject.SetActive(false);
            }
            else
            {
                headerField.gameObject.SetActive(true);
                headerField.text = header;
            }

            contentField.text = content;
        }

	    private void Update()
	    {
            var position = Input.mousePosition;

            var pivotX = position.x / Screen.width;
            var pivotY = position.y / Screen.height;

            rectTransform.pivot = new Vector2(pivotX, pivotY);
            transform.position = position;
	    }
	}
}