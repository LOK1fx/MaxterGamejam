using Photon.Pun;
using UnityEngine;
using System;

namespace com.LOK1game.recode
{
    public abstract class GameModeBase : MonoBehaviour
    {
        public event Action<GameObject> OnPlayerSpawned;

        [Header("Main components")]
        public GameObject hud;
        public GameObject player;
        public GameObject playerController;
        public GameObject playerCamera;
        public GameObject spectator;

        [Space]
        [HideInInspector] public GameObject initializedHud;
        [HideInInspector] public GameObject spawnedPlayer;

        private bool initialized;

        #region events

        public event Action<GameObject> onHudInitialized;

        #endregion

        public virtual void Init()
        {
            if(initialized) { return; }

            Debug.Log("Init");

            InitHud();
            SpawnCamera();

            spawnedPlayer = SpawnPlayer();

            initialized = true;

            Debug.Log(initialized);
        }

        protected virtual GameObject SpawnPlayer()
        {
            OnPlayerSpawned?.Invoke(spawnedPlayer);

            return spawnedPlayer;
        }

        protected virtual void SpawnCamera()
        {
            Instantiate(playerCamera, Vector3.zero, Quaternion.identity).GetComponent<Player.MoveCamera>();
        }

        private void InitHud()
        {
            if(hud == null) { return; }

            var initializedHud = Instantiate(hud);

            if (initializedHud != null)
            {
                this.initializedHud = initializedHud;
                onHudInitialized?.Invoke(initializedHud);
            }
            else
            {
                return;
            }
        }
    }
}
