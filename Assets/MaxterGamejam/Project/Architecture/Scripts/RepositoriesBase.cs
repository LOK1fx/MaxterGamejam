using UnityEngine;
using System.Collections.Generic;
using System;

namespace com.LOK1game.recode.Architecture
{
    public class RepositoriesBase
    {
        private Dictionary<Type, Repository> _repositoriesMap;
        private SceneConfig _sceneConfig;

        public RepositoriesBase(SceneConfig config)
        {
            _sceneConfig = config;
        }

        public void CreateAllRepositories()
        {
            _repositoriesMap = _sceneConfig.CreateAllRepositories();
        }

        public void SendOnCreateToAllRepositories()
        {
            var allInteractors = _repositoriesMap.Values;

            foreach (var r in allInteractors)
            {
                r.OnCreate();
            }
        }

        public void InitializeAllRepositories()
        {
            var allRepositories = _repositoriesMap.Values;

            foreach (var r in allRepositories)
            {
                r.Initialize();
            }
        }

        public void SendOnStartToAllRepositories()
        {
            var allRepositories = _repositoriesMap.Values;

            foreach (var r in allRepositories)
            {
                r.OnStart();
            }
        }

        public T GetRepository<T>() where T : Repository
        {
            var type = typeof(T);

            return (T)_repositoriesMap[type];
        }
    }
}