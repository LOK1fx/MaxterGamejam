using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace com.LOK1game.recode  //LOK1game script behaviour😎💗
{
	public class GamepadButtonIcon : MonoBehaviour
	{
        [SerializeField] private GameObject _icon;

        private void Update()
        {
            _icon.SetActive(Gamepad.current == null ? false : true);
        }
    }
}