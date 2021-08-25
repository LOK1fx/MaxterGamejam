using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.LOK1game.recode;

namespace com.LOK1game.MaxterGamejam
{
    public class Flashlight : PlayerItem
    {
        [SerializeField] private GameObject _light;
        [SerializeField] private AudioClip _turnSound;

        private AudioSource _audio;

        private bool _isOn;

        private void Start()
        {
            var input = PlayerInput.GetInput();

            input.Player.Flashlight.performed += ctx => Use(this);

            _isOn = _light.activeInHierarchy;
            _audio = GetComponent<AudioSource>();
        }

        public override void Use(object sender)
        {
            _audio.PlayOneShot(_turnSound);

            _isOn = !_isOn;

            _light.SetActive(_isOn);
        }
    }
}