using com.LOK1game.MaxterGamejam;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZone : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform[] _spawnPoints;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(13f);

            for (int i = 0; i < _spawnPoints.Length; i++)
            {
                Instantiate(_enemyPrefab, SpawnPosition(i), Quaternion.identity);
            }
        }
    }

    private Vector3 SpawnPosition(int index)
    {
        var pos = _spawnPoints[index].position;
        var ranPos = new Vector3(Random.Range(-3f, 3f), 0f, Random.Range(-3f, 3f));

        if(Physics.Raycast(pos, ranPos.normalized, out RaycastHit hit, ranPos.magnitude, 8))
        {
            pos = hit.point + hit.normal * 1.2f;
        }
        else
        {
            pos += ranPos;
        }

        return pos;
    }
}
