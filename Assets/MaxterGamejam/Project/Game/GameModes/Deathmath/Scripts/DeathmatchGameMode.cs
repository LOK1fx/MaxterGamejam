using System.Linq;
using Photon.Pun;
using UnityEngine;
using com.LOK1game.recode.Player;

namespace com.LOK1game.recode
{
    public class DeathmatchGameMode : GameModeBaseWithSpawns
    {
        public override void Init()
        {
            spawns = FindObjectsOfType<DeathmatchSpawnPoint>().ToArray();

            base.Init();

            MoveCamera.Instance.cameraPosition = spawnedPlayer.GetComponent<Player.Player>().GetCameraTransform();
        }

        protected override GameObject SpawnPlayer()
        {
            var spawnTransform = spawns[Random.Range(0, spawns.Length)].transform;

            return PhotonNetwork.Instantiate(player.name, spawnTransform.position, spawnTransform.rotation);
        }
    }
}