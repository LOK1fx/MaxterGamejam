using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.LOK1game.recode.UI  //LOK1game script behaviour😎💗
{
	public class Speedometer : MonoBehaviour
	{
        public Text text;

        private void Update()
        {
            if(Player.Player.LocalPlayerInstance == null) { return; }

            var speed = Player.Player.LocalPlayerInstance.GetSpeed();

            text.text = $"UPS: {Mathf.RoundToInt(speed * 100f)}";
        }
    }
}