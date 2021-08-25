using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LOK1game.MaxterGamejam.World
{
    public class Magnet : MonoBehaviour
    {
        [SerializeField] private Magnet _otherMagnet;

        [Space]
        [SerializeField] private float _pushingForce;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            var dir = -(_otherMagnet.transform.position - transform.position).normalized;

            _rigidbody.AddForce(dir * _pushingForce, ForceMode.Acceleration);
        }

        private void OnDrawGizmos()
        {
            if(_otherMagnet == null) { return; }

            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position + (Vector3.up * 0.15f), _otherMagnet.transform.position);
        }
    }
}