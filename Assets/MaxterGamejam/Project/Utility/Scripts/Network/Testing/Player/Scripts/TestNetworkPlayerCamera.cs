using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNetworkPlayerCamera : MonoBehaviour
{
    [SerializeField] private NetworkPlayerManager _player;

    [SerializeField] private float _sensitivity = 100f;
    [SerializeField] private float _clampAngle = 85f;

    private float _verticalRotation;
    private float _horizontalRotation;

    private void Start()
    {
        _verticalRotation = transform.localEulerAngles.x;
        _horizontalRotation = _player.transform.eulerAngles.y;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Look();

        Debug.DrawRay(transform.position, transform.forward * 2, Color.red);
    }

    private void Look()
    {
        float _mouseVertical = -Input.GetAxis("Mouse Y");
        float _mouseHorizontal = Input.GetAxis("Mouse X");

        _verticalRotation += _mouseVertical * _sensitivity * Time.deltaTime;
        _horizontalRotation += _mouseHorizontal * _sensitivity * Time.deltaTime;

        _verticalRotation = Mathf.Clamp(_verticalRotation, -_clampAngle, _clampAngle);

        transform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
        _player.transform.rotation = Quaternion.Euler(0f, _horizontalRotation, 0f);
    }
}
