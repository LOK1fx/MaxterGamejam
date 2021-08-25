using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Haptics;

namespace com.LOK1game.recode  //LOK1game script behaviour😎💗
{
	public class GamepadRumble : MonoBehaviour
    {
        private float _timer;

        private void Update()
        {
            if (Gamepad.current == null) { return; }

            if (_timer <= 0f)
            {
                Gamepad.current.SetMotorSpeeds(0f, 0f);
            }
            else
            {
                _timer -= Time.deltaTime;
            }
        }

        public void Rumble(float speed)
        {
            if(Gamepad.current == null) { return; }

            _timer = 1.4f;

            Gamepad.current.SetMotorSpeeds(speed, speed + 0.15f);     
        }
    }
}