using Photon.Pun;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using com.LOK1game.MaxterGamejam;

namespace com.LOK1game.recode
{
    /// <summary>
    /// По сути сетевой компонент, но может использоваться как однопользовательский
    /// </summary>
    public abstract class GameControllerBase : MonoBehaviourPunCallbacks
    {
        public static event Action<GameModeBase> OnGameModeInitialized;

        private static GameModeBase _currentGameMode;

        protected static GameOptions _currentGameOptions;

        private void Awake()
        {
            InitializeGameOptions();
        }


        /// <summary>
        /// Проверка подключения к серверам
        /// </summary>
        /// <returns></returns>
        private bool ValidateConnection()
        {
            if (PhotonNetwork.IsConnected)
            {
                return true;
            }
            else
            {
                Debug.LogError($"Not validate connection {name}");

                return false;
            }
        }

        protected abstract GameOptions InitializeGameOptions();

        public static T TryGetGameOptions<T>(out T options) where T : GameOptions
        {
            options = (T)_currentGameOptions;

            return options;
        }

        /// <summary>
        /// Устанавливает игровой режим
        /// </summary>
        /// <typeparam name="T">Класс игрового режима</typeparam>
        /// <param name="data">Конфиг режима</param>
        public void SetGameMode<T>(GameModeDataBase data) where T : GameModeBase
        {
            if(GameObject.FindGameObjectsWithTag("GameMode") != null)
            {
                var modes = GameObject.FindGameObjectsWithTag("GameMode");

                for (int i = 0; i < modes.Length; i++)
                {
                    Destroy(modes[i].gameObject);
                }
            }

            var go = new GameObject("[ACTIVE GAME MODE]") //Отдельный объект с компонентом режима в сцене нужен для того, что бы можно было получить доступ к режиму и его компонентам
            {
                tag = "GameMode"
            };

            _currentGameMode = go.AddComponent<T>();

            SetGameModeData(data);

            _currentGameMode.Init();

            OnGameModeInitialized?.Invoke(_currentGameMode);
        }

        public static T GetGameMode<T>() where T : GameModeBase
        {
            if (_currentGameMode == null)
            {
                throw new Exception("The are no any Game Mode");
            }
            if(_currentGameMode != (T)_currentGameMode)
            {
                throw new ArgumentException($"The are no {(T)_currentGameMode}");
            }

            return (T)_currentGameMode;
        }

        /// <summary>
        /// Звполняет поля режима в зависимости от настройки переданной
        /// в метод
        /// </summary>
        /// <param name="data">конфиг</param>
        private void SetGameModeData(GameModeDataBase data)
        {
            var mode = _currentGameMode;

            mode.hud = data.hud;
            mode.player = data.player;
            mode.playerController = data.playerController;
            mode.playerCamera = data.playerCamera;
            mode.spectator = data.spectator;
        }

        /// <summary>
        /// Возращает массив предметов, которые должны изменятся в зависимости от
        /// состояния раунда.
        /// </summary>
        public IRoundActor[] GetAllRoundActors()
        {
            IRoundActor[] actors = FindObjectsOfType<MonoBehaviour>().OfType<IRoundActor>().ToArray();

            return actors;
        }

        protected void RestartRound()
        {
            foreach (var i in GetAllRoundActors())
            {
                i.OnRoundEnd();
            }
        }

        protected void StartRound()
        {
            foreach (var i in GetAllRoundActors())
            {
                i.OnRoundStart();
            }
        }
    }
}