using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Rigidbody[] _rigidbodies;

    public Collider[] Colliders { get; private set; }
    public Vector3 Velocity;

    private void Awake()
    {
        Colliders = new Collider[_rigidbodies.Length];

        for (int i = 0; i < _rigidbodies.Length; i++)
        {
            Colliders[i] = _rigidbodies[i].GetComponent<Collider>();
        }

        TurnOff();
    }

    public void TurnOn()
    {
        SetEnabled(true);
    }

    public void TurnOff()
    {
        SetEnabled(false);
    }

    private void SetEnabled(bool enable)
    {
        for (int i = 0; i < _rigidbodies.Length; i++)
        {
            _rigidbodies[i].isKinematic = !enable;
            _rigidbodies[i].useGravity = enable;

            if(TryGetComponent<Animator>(out var animator))
            {
                animator.enabled = !enable;
            }

            if(enable)
            {
                Colliders[i].gameObject.layer = 0;
                _rigidbodies[i].AddForce(Velocity, ForceMode.Impulse);
            }
            else
            {
                Colliders[i].gameObject.layer = 13;
            }
        }

        Velocity = Vector3.zero;
    }
}