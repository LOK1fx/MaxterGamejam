using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LOK1game.recode.Player
{
	public class CameraFov : MonoBehaviour
	{
        [SerializeField] private Camera _camera;

        public float baseFov = 70f;
        public float maxFov = 110f;

        [HideInInspector] public float currentFov;

        [Space]
        public float smooth;

	    private void Start() => _camera.fieldOfView = baseFov;

        private void Update()
        {
            if (Player.LocalPlayerInstance != null && Player.LocalPlayerInstance.GetSpeed() != 0f)
            {
                currentFov = baseFov * Player.LocalPlayerInstance.GetSpeed() / (baseFov / 11f);
            }
            else
            {
                currentFov = baseFov;
            }

            currentFov = Mathf.Clamp(currentFov, baseFov, maxFov);

            _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, currentFov, Time.deltaTime * smooth);
	    }

        public void SetFov(float value)
        {
            baseFov = value;
            maxFov = value + 20f;
        }
	}
}