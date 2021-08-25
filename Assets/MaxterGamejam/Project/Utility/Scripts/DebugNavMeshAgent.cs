using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace LOK1game.Tools.Debugging
{
    public class DebugNavMeshAgent : MonoBehaviour
    {
        public bool ShowVelocity;
        public bool ShowDesiredVelocity;
        public bool ShowPath;

        private NavMeshAgent _navAgent;

        private void Awake()
        {
            _navAgent = GetComponent<NavMeshAgent>();
        }

        private void OnValidate()
        {
            _navAgent = GetComponent<NavMeshAgent>();
        }

        private void OnDrawGizmos()
        {
            if(ShowVelocity)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, transform.position + _navAgent.velocity);
            }
            if (ShowDesiredVelocity)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, transform.position + _navAgent.desiredVelocity);
            }
            if(ShowPath)
            {
                var path = _navAgent.path;
                var prevCorner = transform.position;

                Gizmos.color = Color.black;

                foreach (var corner in path.corners)
                {
                    Gizmos.DrawLine(prevCorner, corner);
                    Gizmos.DrawSphere(corner, 0.1f);

                    prevCorner = corner;
                }
            }
        }
    }
}