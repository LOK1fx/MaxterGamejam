using UnityEngine;
using System.Collections;
using System;

namespace com.LOK1game.recode.Architecture
{
    public static class LocalPlayer
    {
        public static event Action OnLocalPlayerInitialized;

        public static int health
        {
            get
            {
                CheckClass();
                return _playerInteractor.health;
            }
        }

        public static bool isInitialized { get; private set; }

        private static LocalPlayerInteractor _playerInteractor;

        public static void Initialize(LocalPlayerInteractor interactor)
        {
            _playerInteractor = interactor;

            isInitialized = true;

            OnLocalPlayerInitialized?.Invoke();
        }

        public static void AddHealth(object sender, int value)
        {
            CheckClass();
            _playerInteractor.AddHealth(sender, value);
        }

        public static void Damage(object sender, int value)
        {
            CheckClass();
            _playerInteractor.Damage(sender, value);
        }

        private static void CheckClass()
        {
            if (!isInitialized)
            {
                throw new Exception($"LocalPlayer servies not initialize yet");
            }        
        }
    }
}