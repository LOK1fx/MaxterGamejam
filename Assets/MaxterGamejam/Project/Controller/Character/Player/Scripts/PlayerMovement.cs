using LOK1game.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LOK1game.recode.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        #region events

        public event Action OnLand;
        public event Action OnJump;
        public event Action OnStartCrouch;
        public event Action OnStopCrouch;
        public event Action OnStartSlide;

        #endregion

        [Header("Jumping")]
        [SerializeField] private float _jumpCooldown = 0.1f;

        private float _currentJumpCooldown;

        [Header("Physics")]
        [SerializeField] private LayerMask _groundMask;

        protected bool onSlope => OnSlope();
        protected bool onGround;

        private Vector3 _moveDirection;

        private RaycastHit _slopeHit;

        [Header("Sliding")]
        [SerializeField] private float _maxSlideTime;

        private float _currentSlideTime;

        [Header("Components")]
        [SerializeField] private PlayerMoveData _movementData;
        [SerializeField] private Transform _directionTransform;

        private Player _player;

        public Rigidbody Rigidbody { get; private set; }
        private CapsuleCollider _playerCollider;

        private Vector2 _iAxis;

        private void Awake()
        {
            _currentJumpCooldown = _jumpCooldown;

            Rigidbody = GetComponent<Rigidbody>();
            _playerCollider = GetComponent<CapsuleCollider>();
            _player = GetComponent<Player>();
        }

        private void Update()
        {
            UpdateCooldowns();
            Checks();

            if (PlayerState.Sliding && PlayerState.OnGround)
            {
                _currentSlideTime += Time.deltaTime;
            }
            if (_currentSlideTime >= _maxSlideTime)
            {
                Rigidbody.velocity = Vector3.zero;

                StopCrouch();
                StartCrouch();

                _currentSlideTime = 0f;
            }
        }

        public void SetAxisInput(Vector2 input)
        {
            _iAxis = input;
        }

        public void Move()
        {
            if (onGround && Rigidbody.velocity.y <= -4f)
            {
                Land();
            }

            if (!PlayerState.InTransport)
            {
                Vector3 velocity;

                var moveParams = new CharacterMath.MoveParams(GetNonNormDirection(_iAxis), Rigidbody.velocity);
                var slopeMoveParams = new CharacterMath.MoveParams(GetSlopeDirection(_iAxis), Rigidbody.velocity);
                var slideMoveParams = new CharacterMath.MoveParams(Vector3.zero, Rigidbody.velocity);

                if (onGround && !onSlope && !PlayerState.Sliding)
                {
                    velocity = MoveGround(moveParams, PlayerState.Sprinting, PlayerState.Crouching);
                }
                else if (onGround && onSlope && !PlayerState.Sliding)
                {
                    velocity = MoveGround(slopeMoveParams, PlayerState.Sprinting, PlayerState.Crouching);
                    Rigidbody.AddForce(GetSlopeDirection(_iAxis).normalized * 8f, ForceMode.Acceleration);
                }
                else if (PlayerState.Sliding)
                {
                    velocity = MoveAir(slideMoveParams);
                }
                else
                {
                    velocity = MoveAir(moveParams);
                }

                Rigidbody.velocity = velocity;
            }
        }

        public void Jump()
        {
            if (CanJump() && onGround)
            {
                var velocity = new Vector3(Rigidbody.velocity.x, 0f, Rigidbody.velocity.z);

                Rigidbody.velocity = velocity;
                Rigidbody.AddForce(transform.up * _movementData.jumpForce, ForceMode.Impulse);

                MoveCamera.Instance.lerpOffset += Vector3.up * _movementData.jumpForce * 0.02f;

                ResetJumpCooldown();

                OnJump?.Invoke();
            }
        }

        public void StartCrouch()
        {
            if (PlayerState.Wallruning) { return; }

            _currentSlideTime = 0f;

            _player.PlayerHeight = 1.5f;

            _playerCollider.center = Vector3.up * 0.75f;
            _playerCollider.height = _player.PlayerHeight;  

            if (Rigidbody.velocity.magnitude > 6f)
            {
                StartSlide();
            }
            else
            {
                PlayerState.Crouching = true;
            }

            OnStartCrouch?.Invoke();
        }

        public void StartSlide()
        {
            _currentSlideTime = 0f;

            var dir = GetDirection(_iAxis);

            if (onGround)
            {
                Rigidbody.AddForce(dir * 40f, ForceMode.Impulse);
            }
            else
            {
                Rigidbody.AddForce(dir * 10f, ForceMode.Impulse);
            }

            OnStartSlide?.Invoke();

            PlayerState.Sliding = true;
        }

        public void StopCrouch()
        {
            if (Physics.Raycast(transform.position, Vector3.up, out RaycastHit hit, 2f, _groundMask, QueryTriggerInteraction.Ignore))
            {
                return;
            }

            _player.PlayerHeight = 2f;

            _playerCollider.height = _player.PlayerHeight;
            _playerCollider.center = Vector3.up;

            PlayerState.Sliding = false;
            PlayerState.Crouching = false;

            OnStopCrouch?.Invoke();
        }

        public bool CanJump()
        {
            if (_currentJumpCooldown <= 0 && !PlayerState.InTransport)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Вызывается если игрок касается земли после прыжка с достаточной скоростью
        /// </summary>
        private void Land()
        {
            MoveCamera.Instance.lerpOffset += Vector3.up * Rigidbody.velocity.y * 0.12f;

            var velocity = new Vector3(Rigidbody.velocity.x, 0f, Rigidbody.velocity.z);

            Rigidbody.velocity = velocity;

            var dirOffset = Vector3.ClampMagnitude(velocity, 1f);

            MoveCamera.Instance.lerpOffset += dirOffset * 0.35f;

            OnLand?.Invoke();
        }

        private void UpdateCooldowns()
        {
            if (_currentJumpCooldown > 0)
                _currentJumpCooldown -= Time.deltaTime;
        }

        public void ResetJumpCooldown()
        {
            _currentJumpCooldown = _jumpCooldown;
        }

        private void Checks()
        {
            var checkPos = transform.position + transform.up * 0.25f;

            GroundCheck(checkPos);
        }

        private void GroundCheck(Vector3 pos)
        {
            if (Physics.CheckSphere(pos, 0.3f, _groundMask))
            {
                onGround = true;
            }
            else
            {
                onGround = false;
            }

            PlayerState.OnGround = onGround;
        }

        private bool OnSlope()
        {
            if (Physics.Raycast(transform.position + (Vector3.up * 0.2f), -transform.up, out _slopeHit, 0.3f, _groundMask))
            {
                if (_slopeHit.normal != Vector3.up)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public Vector3 GetSlopeDirection(Vector2 input)
        {
            return CharacterMath.Project(GetDirection(input), _slopeHit.normal);
        }

        /// <summary>
        /// Возращает не нормализованое напровление ввода относительно игрока.
        /// Подходит для управления с геймпада
        /// </summary>
        /// <param name="input">Ввод</param>
        /// <returns>Direction</returns>
        public Vector3 GetNonNormDirection(Vector2 input)
        {
            var direction = new Vector3(input.x, 0f, input.y);

            direction = _directionTransform.TransformDirection(direction);

            _moveDirection = direction;

            return direction;
        }

        public Vector3 GetDirection(Vector2 input)
        {
            return GetNonNormDirection(input).normalized;
        }

        public Transform GetDirectionTransform()
        {
            return _directionTransform;
        }

        public int GetRoundedSpeed()
        {
            return Mathf.RoundToInt(GetSpeed());
        }

        public float GetSpeed()
        {
            return new Vector3(Rigidbody.velocity.x, 0f, Rigidbody.velocity.z).magnitude;
        }

        #region Acceleratation

        public Vector3 MoveGround(CharacterMath.MoveParams moveParams, bool sprint = false, bool crouch = false)
        {
            float t_speed = moveParams.previousVelocity.magnitude;

            if (t_speed != 0)
            {
                float drop = t_speed * _movementData.friction * Time.fixedDeltaTime;
                moveParams.previousVelocity *= Mathf.Max(t_speed - drop, 0) / t_speed;
            }

            if (!sprint && !crouch)
            {
                return AccelerateVelocity(_movementData.walkGroundAccelerate, _movementData.walkGroundMaxVelocity, moveParams);
            }
            else if (!crouch)
            {
                return AccelerateVelocity(_movementData.sprintGoundAccelerate, _movementData.sprintGoundMaxVelocity, moveParams);
            }
            else
            {
                return AccelerateVelocity(_movementData.crouchGroundAccelerate, _movementData.crouchGroundMaxVelocity, moveParams);
            }
        }

        public Vector3 MoveAir(CharacterMath.MoveParams moveParams)
        {
            return AccelerateVelocity(_movementData.airAccelerate, _movementData.airMaxVelocity, moveParams);
        }

        private Vector3 AccelerateVelocity(float min, float max, CharacterMath.MoveParams moveParams)
        {
            return CharacterMath.Accelerate(moveParams, min, max, Time.fixedDeltaTime);
        }

        #endregion

        private void OnDrawGizmos()
        {
            if (!onSlope) { return; }

            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, transform.position + _slopeHit.normal * 3f);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + CharacterMath.Project(_moveDirection, _slopeHit.normal) * 3f);
        }
    }
}