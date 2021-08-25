using System;
using UnityEngine;
using Photon.Pun;
using LOK1game.Tools;

namespace com.LOK1game.recode.Player
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public abstract class RigidbodyCharacterBase : MonoBehaviour
    {
        public float playerHeight = 2f;

        protected float _xRotation;
        protected float _yRotation;
        protected float _cameraMaxAngles = 90f;

        protected Vector3 _cameraPositionBasePos;
        protected Vector3 _cameraPositionCrouchPos;

        [Header("Jumping")]
        private readonly float _jumpCooldown = 0.1f;
        private float _currentJumpCooldown;

        [Header("Physics")]
        [SerializeField] protected LayerMask groundMask;

        protected bool onSlope => OnSlope();
        protected bool onGround;

        private Vector3 moveDirection;

        private RaycastHit _slopeHit;

        [Header("Components")]
        [SerializeField] private PlayerMoveData _movementData;
        [SerializeField] protected Transform _cameraPosition;
        [SerializeField] protected Transform _directionTransform;

        protected Rigidbody rb;
        protected CapsuleCollider playerCollider;

        #region events

        public event Action OnLand;
        public event Action OnJump;

        #endregion

        protected virtual void Awake()
        {
            _currentJumpCooldown = _jumpCooldown;

            rb = GetComponent<Rigidbody>();
            playerCollider = GetComponent<CapsuleCollider>();

            BindInputs();
        }

        protected virtual void BindInputs()
        {
            if (!PlayerInput.initialized)
            {
                PlayerInput.Init();
            }
        }

        protected virtual void Update()
        {
            UpdateCooldowns();
        }

        protected virtual void LateUpdate()
        {
            Look();
        }

        protected virtual void FixedUpdate()
        {
            Checks();
            Movement();
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(_cameraPosition.rotation);
                stream.SendNext(_directionTransform.rotation);
            }
            else
            {
                _cameraPosition.rotation = (Quaternion)stream.ReceiveNext();
                _directionTransform.rotation = (Quaternion)stream.ReceiveNext();
            }
        }

        protected virtual void UpdateCooldowns()
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
            if (Physics.CheckSphere(pos, 0.3f, groundMask))
            {
                onGround = true;
            }
            else
            {
                onGround = false;
            }

            PlayerState.onGround = onGround;
        }

        private bool OnSlope()
        {
            if (Physics.Raycast(transform.position + (Vector3.up * 0.2f), -transform.up, out _slopeHit, 0.3f, groundMask))
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
            return Character.Project(GetDirection(input), _slopeHit.normal);
        }


        /// <summary>
        /// Вызывается локально каждый физический апдейт
        /// </summary>
        protected virtual void Movement()
        {
            if (onGround && rb.velocity.y <= -4f)
            {
                Land();
            }
        }

        /// <summary>
        /// Вызывается если игрок касается земли после прыжка с достаточной скоростью
        /// </summary>
        protected virtual void Land()
        {
            MoveCamera.Instance.lerpOffset += Vector3.up * rb.velocity.y * 0.12f;

            var velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            rb.velocity = velocity;

            var dirOffset = Vector3.ClampMagnitude(velocity, 1f);

            MoveCamera.Instance.lerpOffset += dirOffset * 0.25f;

            OnLand?.Invoke();
        }

        public virtual void AddCameraPitch(float value)
        {
            _xRotation -= value;

            _xRotation = Mathf.Clamp(_xRotation, -_cameraMaxAngles, _cameraMaxAngles);
        }

        public int GetRoundedSpeed()
        {
            return Mathf.RoundToInt(GetSpeed());
        }

        public float GetSpeed()
        {
            return new Vector3(rb.velocity.x, 0f, rb.velocity.z).magnitude;
        }

        public Rigidbody GetRigidbody()
        {
            return rb;
        }

        public Transform GetCameraTransform()
        {
            return _cameraPosition;
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

            moveDirection = direction;

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

        protected virtual void Look()
        {

        }

        public virtual void Jump()
        {
            if (CanJump() && onGround)
            {
                var velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

                rb.velocity = velocity;
                rb.AddForce(transform.up * _movementData.jumpForce, ForceMode.Impulse);

                MoveCamera.Instance.lerpOffset += Vector3.up * _movementData.jumpForce * 0.02f;

                ResetJumpCooldown();

                OnJump?.Invoke();
            }
        }

        public virtual bool CanJump()
        {
            if (_currentJumpCooldown <= 0 && !PlayerState.inTransport)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region Acceleratation

        public Vector3 MoveGround(Character.MoveParams moveParams, bool sprint = false, bool crouch = false)
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

        public Vector3 MoveAir(Character.MoveParams moveParams)
        {
            return AccelerateVelocity(_movementData.airAccelerate, _movementData.airMaxVelocity, moveParams);
        }

        private Vector3 AccelerateVelocity(float min, float max, Character.MoveParams moveParams)
        {
            return Character.Accelerate(moveParams, min, max, Time.fixedDeltaTime);
        }

        #endregion

#if UNITY_EDITOR

        [SerializeField] private Mesh _EDITOR_CHARACTER_MESH;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireMesh(_EDITOR_CHARACTER_MESH, transform.position + Vector3.up, transform.rotation, Vector3.one);

            var campos = transform.position + new Vector3(0f, 1.5f, 0f);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(campos, campos + transform.forward * 2f);

            if (!onSlope) { return; }

            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, transform.position + _slopeHit.normal * 3f);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + Character.Project(moveDirection, _slopeHit.normal) * 3f);
        }


#endif
    }
}