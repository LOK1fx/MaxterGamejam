﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace com.LOK1game.MaxterGamejam
{
    public class TransitionLoad : MonoBehaviour
    {
        public static TransitionLoad Instance { get; set; }

        public static bool IsLoading { get; private set; } = false;

        private static bool _shouldPlayOpeningAnimation = false;

        private Animator _animator;
        private AsyncOperation _loadingOperation;

        private void Start()
        {
            Instance = this;

            _animator = GetComponent<Animator>();

            if (_shouldPlayOpeningAnimation)
            {
                _animator.SetTrigger("SceneOpen");
                IsLoading = false;
                _shouldPlayOpeningAnimation = false;
            }
        }

        public static void SwitchToScene(string sceneName)
        {
            if(IsLoading) { return; }

            IsLoading = true;
            Instance._animator.SetTrigger("SceneClose");
            Instance._loadingOperation = SceneManager.LoadSceneAsync(sceneName);
            Instance._loadingOperation.allowSceneActivation = false;
        }

        public void OnAnimationOver()
        {
            _shouldPlayOpeningAnimation = true;
            _loadingOperation.allowSceneActivation = true;
        }
    }
}