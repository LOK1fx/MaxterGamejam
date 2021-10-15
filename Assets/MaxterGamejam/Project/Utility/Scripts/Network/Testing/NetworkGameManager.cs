using LOK1game.Tools.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkGameManager : MonoBehaviour
{
    public static NetworkGameManager Instance { get; private set; }

    public static Dictionary<int, NetworkPlayerManager> Players = new Dictionary<int, NetworkPlayerManager>();


    [SerializeField] private GameObject _localPlayerPrefab;
    [SerializeField] private GameObject _worldPlayerPrefab;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(this);
        }
    }

    public static void SetPlayerPosition(int id, Vector3 position)
    {
        Players[id].transform.position = position;
    }

    public void SpawnPlayer(int id, string username, Vector3 position, Quaternion rotation)
    {
        GameObject player;

        if(id == Client.Instance.LocalId)
        {
            player = Instantiate(_localPlayerPrefab, position, rotation);
        }
        else
        {
            player = Instantiate(_worldPlayerPrefab, position, rotation);
        }

        var playerManager = player.GetComponent<NetworkPlayerManager>();

        playerManager.Id = id;
        playerManager.Username = username;

        Players.Add(id, playerManager);
    }
}
