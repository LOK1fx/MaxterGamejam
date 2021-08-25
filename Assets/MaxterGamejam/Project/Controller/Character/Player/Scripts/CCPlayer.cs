using UnityEngine;
using System.Collections;

namespace com.LOK1game.recode.Player
{
    public class CCPlayer : CharacterControllerCharacterBase
    {
        public float sensivity;

        [Space]
        [SerializeField] private Transform _camera;

        private ControlsAction _input;
        private Vector2 _iAxis;
        private Vector3 _iLookDelta;

        private float _cameraPith;
        private float _cameraMaxAngles = 90f;

        protected override void InitializeInput()
        {
            base.InitializeInput();

            _input = new ControlsAction();
            _input.Enable();

            _input.Player.Move.performed += ctx => _iAxis = ctx.ReadValue<Vector2>();
            _input.Player.Look.performed += ctx => _iLookDelta = ctx.ReadValue<Vector2>();
        }

        protected override void Movement()
        {
            base.Movement();

            controller.Move(GetDirection() * _moveData.walkGroundAccelerate * Time.deltaTime);
        }

        protected override void Look()
        {
            base.Look();

            _cameraPith -= _iLookDelta.y * sensivity;
            _cameraPith = Mathf.Clamp(_cameraPith, -_cameraMaxAngles, _cameraMaxAngles);

            _camera.localEulerAngles = Vector3.right * _cameraPith;
            transform.Rotate(transform.up * _iLookDelta.x * sensivity);
        }

        private Vector3 GetDirection()
        {
            var direction = new Vector3(_iAxis.x, 0f, _iAxis.y);

            direction.Normalize();
            direction = transform.TransformDirection(direction);

            return direction;
        }
    }
}