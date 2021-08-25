using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LOK1game.MaxterGamejam
{
    public class ItemsBobbing : MonoBehaviour
    {
        public Vector3 TargetPos { get; set; }

        private Vector3 _currentTargetPos;

        private readonly float _returnSpeed = 10f;

        private void LateUpdate()
        {
            _currentTargetPos = Vector3.Lerp(_currentTargetPos, TargetPos, Time.deltaTime * _returnSpeed);

            transform.localPosition = Vector3.Lerp(transform.localPosition, _currentTargetPos, Time.deltaTime * _returnSpeed);
        }
    }
}