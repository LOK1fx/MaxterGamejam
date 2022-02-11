using UnityEngine;
using UnityEngine.InputSystem;

namespace com.LOK1game.recode.Player
{
    public class PlayerWallrun : PlayerAbilityBase
    {
        [SerializeField] private LayerMask _wallsMask;

        private readonly float _tilt = 15f;
        private readonly float _gravity = 13f;
        private readonly float _wallDistance = 0.72f;
        private readonly float _minJumpHeight = 1.2f;
        private readonly float _cameraTiltSpeed = 5f;
        private readonly float _forwardSpeed = 10f;

        [SerializeField] private bool _proMode; //настройка позаол€юща€ более четко управл€ть бегом по стенам игроку

        private bool _wallOnLeft;
        private bool _wallOnRight;

        private bool _canWallrun;

        private Vector2 _iAxis;

        private RaycastHit _leftWallHit;
        private RaycastHit _rightWallHit;

        private Rigidbody _rb;
        private Player _player;
        private ControlsAction _input;
        private Transform _directionTransform;

        private void Awake() => _rb = GetComponent<Rigidbody>();

        private void Start()
        {
            _player = GetComponent<Player>();

            _directionTransform = _player.PlayerMovement.GetDirectionTransform();

            _input = _player.GetInputClass();
            InitInput();
        }

        private void InitInput()
        {
            _input.Player.Move.performed += ctx => _iAxis = ctx.ReadValue<Vector2>().normalized;
            _input.Player.Jump.performed += JumpFromWall;
        }

        private void Update()
        {
            CheckWall();
            UpdateCameraFx();

            _canWallrun = CanWallRun();
        }

        private void FixedUpdate()
        {
            if (_canWallrun)
            {
                if (_wallOnLeft)
                {
                    StartWallrun();
                }
                else if (_wallOnRight)
                {
                    StartWallrun();
                }
                else
                {
                    StopWallrun();
                }
            }
            else
            {
                StopWallrun();
            }
        }

        private void UpdateCameraFx()
        {
            if (PlayerState.Wallruning)
            {
                if (_wallOnLeft)
                {
                    _player.wallrunTilt = Mathf.Lerp(_player.wallrunTilt, -_tilt, Time.deltaTime * _cameraTiltSpeed);
                }
                else if (_wallOnRight)
                {
                    _player.wallrunTilt = Mathf.Lerp(_player.wallrunTilt, _tilt, Time.deltaTime * _cameraTiltSpeed);
                }
            }
            else
            {
                _player.wallrunTilt = Mathf.Lerp(_player.wallrunTilt, 0f, Time.deltaTime * _cameraTiltSpeed);
            }
        }

        private void CheckWall()
        {
            var pos = transform.position + Vector3.up;

            if (_proMode)
            {
                if (_iAxis.x >= 0.3f)
                {
                    _wallOnRight = Physics.Raycast(pos, _directionTransform.right, out _rightWallHit, _wallDistance, _wallsMask);

                    _wallOnLeft = false;

                    Debug.DrawRay(pos, transform.right * 2f, Color.green);
                }
                else if (_iAxis.x <= -0.3f)
                {
                    _wallOnLeft = Physics.Raycast(pos, -_directionTransform.right, out _leftWallHit, _wallDistance, _wallsMask);

                    _wallOnRight = false;

                    Debug.DrawRay(pos, -_directionTransform.right * 2f, Color.green);
                }
                else
                {
                    _wallOnRight = false;
                    _wallOnLeft = false;
                }
            }
            else
            {
                _wallOnRight = Physics.Raycast(pos, _directionTransform.right, out _rightWallHit, _wallDistance, _wallsMask);
                _wallOnLeft = Physics.Raycast(pos, -_directionTransform.right, out _leftWallHit, _wallDistance, _wallsMask);
            }
        }

        private void JumpFromWall(InputAction.CallbackContext context)
        {
            if (_player.PlayerMovement.CanJump() && PlayerState.Wallruning)
            {
                Vector3 dir;

                if (_wallOnLeft)
                {
                    dir = transform.up + _leftWallHit.normal;
                }
                else
                {
                    dir = transform.up + _rightWallHit.normal;
                }

                _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
                _rb.AddForce(dir * 36f, ForceMode.Impulse);

                _player.PlayerMovement.ResetJumpCooldown();
            }
        }

        private void StartWallrun()
        {
            PlayerState.Wallruning = true;

            _rb.useGravity = false;

            _rb.AddForce(Vector3.down * _gravity, ForceMode.Acceleration);
            _rb.AddForce(_directionTransform.forward * _forwardSpeed, ForceMode.Acceleration);
        }

        private void StopWallrun()
        {
            PlayerState.Wallruning = false;

            _rb.useGravity = true;
        }

        private bool CanWallRun()
        {
            if (_proMode)
            {
                if (_iAxis.x == 0 && !PlayerState.Sliding && !PlayerState.Crouching) { return false; }
            }

            Debug.DrawRay(transform.position + Vector3.up, Vector3.down * _minJumpHeight, Color.green);

            return !Physics.Raycast(transform.position + Vector3.up, Vector3.down, _minJumpHeight, _wallsMask);
        }
    }
}