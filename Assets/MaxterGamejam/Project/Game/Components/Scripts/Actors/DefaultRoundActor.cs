using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LOK1game.recode
{
    /// <summary>
    /// Базовый класс объекта, которому нужно вернуть значения
    /// при старте нового раунда.
    /// </summary>
    public class DefaultRoundActor : MonoBehaviour, IRoundActor
    {
        private Vector3 _startPosition;
        private Quaternion _startRotation;
        private bool _active;

        private Rigidbody rb;

        private void Start()
        {
            _startPosition = transform.position;
            _startRotation = transform.rotation;
            _active = gameObject.activeInHierarchy;

            rb = GetComponent<Rigidbody>();
        }

        public void OnFreezeTimeEnd()
        {
        }

        public void OnRoundEnd()
        {
        }

        public void OnRoundStart()
        {
            if(rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            transform.position = _startPosition;
            transform.rotation = _startRotation;

            gameObject.SetActive(_active);
        }
    }
}