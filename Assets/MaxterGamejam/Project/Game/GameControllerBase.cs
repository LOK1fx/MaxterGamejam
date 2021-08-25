using Photon.Pun;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using com.LOK1game.MaxterGamejam;

namespace com.LOK1game.recode
{
    /// <summary>
    /// �� ���� ������� ���������, �� ����� �������������� ��� ��������������������
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
        /// �������� ����������� � ��������
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
        /// ������������� ������� �����
        /// </summary>
        /// <typeparam name="T">����� �������� ������</typeparam>
        /// <param name="data">������ ������</param>
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

            var go = new GameObject("[ACTIVE GAME MODE]") //��������� ������ � ����������� ������ � ����� ����� ��� ����, ��� �� ����� ���� �������� ������ � ������ � ��� �����������
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
        /// ��������� ���� ������ � ����������� �� ��������� ����������
        /// � �����
        /// </summary>
        /// <param name="data">������</param>
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
        /// ��������� ������ ���������, ������� ������ ��������� � ����������� ��
        /// ��������� ������.
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