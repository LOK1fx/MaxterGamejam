using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game.Tools
{
    public class MeshColliderGizmos : MonoBehaviour
    {
        [SerializeField] private Color _gizmoColor = Color.green;
        [SerializeField] private Mesh _mesh;

        private void OnValidate()
        {
            if(TryGetComponent<MeshCollider>(out var collider))
            {
                _mesh = collider.sharedMesh;
            }
        }

        private void OnDrawGizmos()
        {
            if(_mesh == null) { return; }

            Gizmos.color = new Color(_gizmoColor.r, _gizmoColor.g, _gizmoColor.b, _gizmoColor.a * 0.1f);
            Gizmos.DrawWireMesh(_mesh, 0, transform.position, transform.rotation, transform.localScale);
        }
    }
}