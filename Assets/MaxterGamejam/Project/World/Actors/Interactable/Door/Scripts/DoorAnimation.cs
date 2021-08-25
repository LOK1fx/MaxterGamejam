using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LOK1game.recode.World
{
    [RequireComponent(typeof(Animator))]
    public class DoorAnimation : MonoBehaviour
    {
        private Animator _animator;

        private const string OPEN_TRIGGER_NAME = "Open";
        private const string CLOSE_TRIGGER_NAME = "Close";

        private void Awake()
        {
            var door = GetComponent<Door>();

            door.OnOpen += OnDoorOpen;
            door.OnClose += OnDoorClose;
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnDoorOpen()
        {
            _animator.SetTrigger(OPEN_TRIGGER_NAME);
        }

        private void OnDoorClose()
        {
            _animator.SetTrigger(CLOSE_TRIGGER_NAME);
        }   
    }
}