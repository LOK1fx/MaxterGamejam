using UnityEngine;
using System;
using System.Collections;
using com.LOK1game.recode.Architecture;
using LOK1game.Tools;

namespace com.LOK1game.recode
{
    public static class Game
    {
        public static event Action OnGameInitialized;

        public static SceneManagerBase sceneManager { get; private set; }
    
        public static void Run()
        {
            sceneManager = new SceneManagerExample();

            Coroutines.StartRoutine(InitializeGameRoutine());
        }

        private static IEnumerator InitializeGameRoutine()
        {
            sceneManager.InitScenesMap();

            yield return sceneManager.LoadCurrentSceneAsync();

            OnGameInitialized?.Invoke();
        }

        public static T GetInteractor<T>() where T : Interactor
        {
            return sceneManager.GetInteractor<T>();
        }

        public static T GetRepository<T>() where T : Repository
        {
            return sceneManager.GetRepository<T>();
        }
    }
}