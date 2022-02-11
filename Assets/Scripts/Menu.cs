using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LOK1game
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private MenuAssets _assets;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            //transform.GetChild(0).GetComponent<Image>().sprite = _assets.Background;
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}