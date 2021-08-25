using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.LOK1game.recode;

public class SpawnPlayerItmes : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;

    private void Start()
    {
        Instantiate(_prefab, transform);

        //GameControllerBase.OnGameModeInitialized += OnGameModeInitialized;
    }

    private void OnGameModeInitialized(GameModeBase obj)
    {
        //obj.OnPlayerSpawned += OnPlayerSpawned;
    }

    private void OnPlayerSpawned(GameObject obj)
    {
        //Instantiate(_prefab, transform);
    }
}
