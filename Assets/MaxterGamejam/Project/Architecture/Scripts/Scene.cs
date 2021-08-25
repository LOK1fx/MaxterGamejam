using UnityEngine;
using System.Collections;
using LOK1game.Tools;

namespace com.LOK1game.recode.Architecture
{
    public class Scene
    {
        private InteractorsBase _interactorsBase;
        private RepositoriesBase _repositoriesBase;
        private SceneConfig _sceneConfig;

        public Scene(SceneConfig config)
        {
            _sceneConfig = config;

            _interactorsBase = new InteractorsBase(config);
            _repositoriesBase = new RepositoriesBase(config);
        }

        public Coroutine InitializeAsync()
        {
            return Coroutines.StartRoutine(InitializeRoutine());
        }

        public T GetRepository<T>() where T : Repository
        {
            return _repositoriesBase.GetRepository<T>();
        }

        public T GetInteractor<T>() where T : Interactor
        {
            return _interactorsBase.GetInteractor<T>();
        }

        private IEnumerator InitializeRoutine()
        {
            _interactorsBase.CreateAllInteractors();
            _repositoriesBase.CreateAllRepositories();

            yield return null;

            _interactorsBase.SendOnCreateToAllInteractors();
            _repositoriesBase.SendOnCreateToAllRepositories();

            yield return null;

            _interactorsBase.InitializeAllInteractors();
            _repositoriesBase.InitializeAllRepositories();

            yield return null;

            _interactorsBase.SendOnStartToAllInteractors();
            _repositoriesBase.SendOnStartToAllRepositories();
        }
    }
}