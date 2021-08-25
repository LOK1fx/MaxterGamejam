using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace com.LOK1game.recode.Architecture
{
    public class TestMapConfig : SceneConfig
    {
        public const string SCENE_NAME = "TestLevel";

        public override string sceneName => SCENE_NAME;

        public override Dictionary<Type, Interactor> CreateAllInteractors()
        {
            var interactorsMap = new Dictionary<Type, Interactor>();

            CreateInteractor<LocalPlayerInteractor>(interactorsMap);

            return interactorsMap;
        }

        public override Dictionary<Type, Repository> CreateAllRepositories()
        {
            var repositoriesMap = new Dictionary<Type, Repository>();

            CreateRepository<LocalPlayerRepository>(repositoriesMap);

            return repositoriesMap;
        }
    }
}