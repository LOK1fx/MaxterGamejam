using System.Linq;
using Photon.Pun;
using UnityEngine;
using com.LOK1game.recode.Player;

namespace com.LOK1game.recode
{
    public class GamejamGameMode : GameModeBaseWithSpawns
    {
        public override void Init()
        {
            spawns = FindObjectsOfType<GamejamSpawnPoint>().ToArray();

            base.Init();

            MoveCamera.Instance.cameraPosition = spawnedPlayer.GetComponent<Player.Player>().GetCameraTransform();
        }

        protected override GameObject SpawnPlayer()
        {
            var spawnTransform = spawns[Random.Range(0, spawns.Length)].transform;

            spawnedPlayer = Instantiate(player, spawnTransform.position, spawnTransform.rotation);

            base.SpawnPlayer();

            return spawnedPlayer;
        }
    }
}