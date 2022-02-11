using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayerManager : MonoBehaviour
{
    public int Id;
    public string Username;

    public Vector3 ServerPosition;
    public Vector3 ServerVelocity;
    public long LastServerTick;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(ServerPosition + Vector3.up, Vector3.one + Vector3.up);
    }
}