using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.LOK1game.recode.Player;

namespace com.LOK1game.MaxterGamejam
{
    public class GunMidlePoints : MonoBehaviour
    {
        [SerializeField] private Transform _point1;
        [SerializeField] private Transform _point2;

        private void Start()
        {
            _point2 = MoveCamera.Instance.transform;
        }

        private void LateUpdate()
        {
            if(_point1 != null || _point2 != null) { return; }

            transform.position = (_point1.position + _point2.position) / 2;
        }
    }
}