using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace com.LOK1game.recode.Architecture
{
    public class InteractorsBase
    {
        private Dictionary<Type, Interactor> _interactorsMap;
        private SceneConfig _sceneConfig;

        public InteractorsBase(SceneConfig config)
        {
            _sceneConfig = config;
        }

        public void CreateAllInteractors()
        {
            _interactorsMap = _sceneConfig.CreateAllInteractors();
        }

        public void SendOnCreateToAllInteractors()
        {
            var allInteractors = _interactorsMap.Values;

            foreach (var i in allInteractors)
            {
                i.OnCreate();
            }
        }

        public void InitializeAllInteractors()
        {
            var allInteractors = _interactorsMap.Values;

            foreach (var i in allInteractors)
            {
                i.Initialize();
            }
        }

        public void SendOnStartToAllInteractors()
        {
            var allInteractors = _interactorsMap.Values;

            foreach (var i in allInteractors)
            {
                i.OnStart();
            }
        }

        public T GetInteractor<T>() where T : Interactor
        {
            var type = typeof(T);

            return (T)_interactorsMap[type];
        }
    }
}