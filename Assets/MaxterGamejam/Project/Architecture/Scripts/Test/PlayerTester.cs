using UnityEngine;
using System.Collections;
using System;
using UnityEngine.InputSystem;
using com.LOK1game.recode.Architecture;

namespace com.LOK1game.recode.Player
{
    public class PlayerTester : MonoBehaviour
    {
        private ControlsAction _input;

        private void Start()
        {
            Game.Run();

            _input = new ControlsAction();
            _input.Enable();

            _input.Player.Jump.performed += Damage;
            _input.Player.Fire.performed += HealthUp;
        }

        private void Damage(InputAction.CallbackContext ctx)
        {
            LocalPlayer.Damage(this, 15);

            Debug.Log($"Health: {LocalPlayer.health}");
        }

        private void HealthUp(InputAction.CallbackContext ctx)
        {
            LocalPlayer.AddHealth(this, 20);

            Debug.Log($"Health: {LocalPlayer.health}");
        }
    }
}