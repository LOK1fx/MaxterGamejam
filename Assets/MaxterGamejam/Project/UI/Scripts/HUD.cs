using com.LOK1game.recode.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.LOK1game.MaxterGamejam
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;
        [SerializeField] private Transform _hintTransform;

        public void Start()
        {
            var player = Player.LocalPlayerInstance;

            if(player != null)
            {
                player.OnHealthChanged += OnPlayerHealthChanged;
            }
        }

        private void OnPlayerHealthChanged(int health)
        {
            _healthBar.fillAmount = health * 0.01f;
        }

        public Transform GetHintsTransform()
        {
            return _hintTransform;
        }
    }
}