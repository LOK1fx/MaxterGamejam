using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using LOK1game.Tools;

namespace com.LOK1game.recode.Architecture
{
    public abstract class SceneManagerBase
    {
        public event Action<Scene> OnSceneLoaded;

        public Scene scene { get; private set; }
        public bool isLoading { get; private set; }

        protected Dictionary<string, SceneConfig> sceneConfigMap;

        public SceneManagerBase()
        {
            this.sceneConfigMap = new Dictionary<string, SceneConfig>();
        }

        public abstract void InitScenesMap();


        public Coroutine LoadCurrentSceneAsync()
        {
            if (isLoading)
            {
                throw new Exception("Some scene is arleady loading");
            }

            var sceneName = SceneManager.GetActiveScene().name;
            var config = sceneConfigMap[sceneName];

            return Coroutines.StartRoutine(LoadCurrentSceneRoutine(config));
        }

        private IEnumerator LoadCurrentSceneRoutine(SceneConfig config)
        {
            isLoading = true;

            yield return Coroutines.StartRoutine(InitializeSceneRoutine(config));

            isLoading = false;
            OnSceneLoaded?.Invoke(scene);
        }

        public Coroutine LoadNewSceneAsync(string sceneName)
        {
            if(isLoading)
            {
                throw new Exception("Some scene is arleady loading");
            }

            var config = sceneConfigMap[sceneName];

            return Coroutines.StartRoutine(LoadNewSceneRoutine(config));
        }

        private IEnumerator LoadNewSceneRoutine(SceneConfig config)
        {
            isLoading = true;

            yield return Coroutines.StartRoutine(LoadSceneRoutine(config));

            yield return Coroutines.StartRoutine(InitializeSceneRoutine(config));

            isLoading = false;
            OnSceneLoaded?.Invoke(scene);
        }

        private IEnumerator LoadSceneRoutine(SceneConfig config)
        {
            var async = SceneManager.LoadSceneAsync(config.sceneName);

            async.allowSceneActivation = false;

            while(async.progress < 0.9f)
            {
                yield return null;
            }

            async.allowSceneActivation = true;
        }

        private IEnumerator InitializeSceneRoutine(SceneConfig config)
        {
            scene = new Scene(config);

            yield return scene.InitializeAsync();
        }

        public T GetRepository<T>() where T : Repository
        {
            return scene.GetRepository<T>();
        }

        public T GetInteractor<T>() where T : Interactor
        {
            return scene.GetInteractor<T>();
        }
    }
}