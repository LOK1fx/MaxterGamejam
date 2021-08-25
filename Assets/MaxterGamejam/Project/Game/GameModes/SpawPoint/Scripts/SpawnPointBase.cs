using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LOK1game.recode  //LOK1game script behaviour😎💗
{
	public abstract class SpawnPointBase : MonoBehaviour
	{
        [HideInInspector] public bool busy;

        private void OnTriggerExit(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                busy = false;
            }    
        }

#if UNITY_EDITOR

        [SerializeField] private bool _drawMesh;
        [SerializeField] private Mesh _mesh;

        [Space]
        [SerializeField] private Vector3 _position = Vector3.up;
        [SerializeField] private Quaternion _rotation = new Quaternion(0f, 1f, 0f, 0f);
        [SerializeField] private Vector3 _size = new Vector3(1f, 2f, 1f);

        [Space]
        [SerializeField] private Vector3 _cameraPosition = Vector3.up;

        private void OnDrawGizmos()
        {
            if (_drawMesh && _mesh != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawMesh(_mesh, transform.position + _position, transform.rotation * _rotation);
            }
            else
            {
                Gizmos.color = Color.white;
                Gizmos.DrawWireCube(transform.position + _position, _size);

                Gizmos.color = Color.yellow;
                Gizmos.DrawRay(transform.position + _cameraPosition, transform.forward);
            }
        }

#endif
    }
}