using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private Vector3 _rotateAxis;

    private void Update()
    {
        transform.Rotate(_rotateAxis, Time.deltaTime * _rotateAxis.magnitude);
    }
}
