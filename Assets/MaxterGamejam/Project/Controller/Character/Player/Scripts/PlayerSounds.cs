using UnityEngine;

namespace com.LOK1game.recode.Player
{
    public class PlayerSounds : CharacterSounds
    {
        [Header("Slide Fx")]
        [SerializeField] private AudioClip _startSlideClip;
        [SerializeField] private Player _player;

        protected override void Start()
        {
            base.Start();

            _player.OnStartSlide += OnPlayerStartSlide;
        }

        private void OnPlayerStartSlide()
        {
            audio.PlayOneShot(_startSlideClip);
        }
    }
}