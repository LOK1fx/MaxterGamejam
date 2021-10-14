using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LOK1game.MaxterGamejam
{
    [RequireComponent(typeof(LineRenderer))]
    public class BulletTrail : MonoBehaviour
    {
        [SerializeField] private float _speed = 15f;

        private LineRenderer _trail;

        private void Awake()
        {
            _trail = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            _trail.SetPosition(0, Vector3.MoveTowards(_trail.GetPosition(0), _trail.GetPosition(1), Time.deltaTime * _speed));
        }
    }
}