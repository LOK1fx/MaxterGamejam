using System;
using UnityEngine;
using UnityEngine.InputSystem;
using LOK1game.Tools;
using LOK1game;
using System.Collections;
using com.LOK1game.MaxterGamejam;
using UnityEngine.SceneManagement;

namespace com.LOK1game.recode.Player
{
    public class Player : RigidbodyCharacterBase, IDamagable
    {
        public static Player LocalPlayerInstance { get; set; }

        public event Action<int> OnHealthChanged;

        public int Health { get; private set; }

        [SerializeField] private int _maxHealth = 100;

        [Header("Camera")]
        [SerializeField] private float _sensivity;

        [Header("Tilts")]
        [HideInInspector] public float wallrunTilt;

        private readonly float _slideTiltSmooth = 8f;
        private float _slideTilt;
        private float _currentSlideTilt;

        [Header("Other")]
        [SerializeField] private float _interactionDistance = 2f;
        [SerializeField] private LayerMask _interactionMask;

        [Header("Sliding")]
        [SerializeField] private float _maxSlideTime;

        private float _currentSlideTime;

        [Space]
        private ControlsAction _input;
        private Vector2 _iAxis;
        private Vector3 _iLookDelta;

        #region Events

        public event Action OnStartSlide;

        #endregion

        protected override void Awake()
        {
            base.Awake();

            Health = _maxHealth;

            LocalPlayerInstance = this;
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            StartCoroutine(HoverActors());

            _cameraPositionBasePos = _cameraPosition.localPosition;
            _cameraPositionCrouchPos = _cameraPositionBasePos + Vector3.down * 0.5f;
        }

        protected override void BindInputs()
        {
            base.BindInputs();

            _input = PlayerInput.GetInput();

            _input.Player.Move.performed += ctx => _iAxis = ctx.ReadValue<Vector2>();
            _input.Player.Look.performed += ctx => _iLookDelta = ctx.ReadValue<Vector2>();
            _input.Player.Fire.performed += ctx => AddCameraPitch(1f);
            _input.Player.Jump.performed += ctx => Jump();

            _input.Player.Interact.performed += Interact;

            _input.Player.Crouch.started += ctx => LocalStartCrouch();
            _input.Player.Crouch.canceled += ctx => LocalStopCrouch();

            _input.Player.Sprint.performed += ctx => PlayerState.sprinting = !PlayerState.sprinting;

            _sensivity = Settings.GetSensivity();
        }

        protected override void Update()
        {
            base.Update();

            if (PlayerState.sliding && onGround)
            {
                _currentSlideTime += Time.deltaTime;
            }
            if (_currentSlideTime >= _maxSlideTime)
            {
                rb.velocity = Vector3.zero;

                LocalStopCrouch();
                LocalStartCrouch();

                _currentSlideTime = 0f;
            }

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                TransitionLoad.SwitchToScene("Menu");
            }

            UpdateFX();
        }

        private void UpdateFX()
        {
            _currentSlideTilt = Mathf.Lerp(_currentSlideTilt, _slideTilt, Time.deltaTime * _slideTiltSmooth);
        }

        protected override void Movement()
        {
            base.Movement();

            if (!PlayerState.inTransport)
            {
                Vector3 velocity;

                var moveParams = new Character.MoveParams(GetNonNormDirection(_iAxis), rb.velocity);
                var slopeMoveParams = new Character.MoveParams(GetSlopeDirection(_iAxis), rb.velocity);
                var slideMoveParams = new Character.MoveParams(Vector3.zero, rb.velocity);

                if (onGround && !onSlope && !PlayerState.sliding)
                {
                    velocity = MoveGround(moveParams, PlayerState.sprinting, PlayerState.crouching);
                }
                else if (onGround && onSlope && !PlayerState.sliding)
                {
                    velocity = MoveGround(slopeMoveParams, PlayerState.sprinting, PlayerState.crouching);
                    rb.AddForce(GetSlopeDirection(_iAxis).normalized * 8f, ForceMode.Acceleration);
                }
                else if (PlayerState.sliding)
                {
                    velocity = MoveAir(slideMoveParams);
                }
                else
                {
                    velocity = MoveAir(moveParams);
                }

                rb.velocity = velocity;
            }
        }

        protected override void Land()
        {
            base.Land();

            if (_input.Player.Jump.phase == InputActionPhase.Started)
            {
                Jump();
            }
        }

        private void LocalStartCrouch()
        {
            StartCrouch();
        }

        private void LocalStopCrouch()
        {
            StopCrouch();
        }

        private void StartCrouch()
        {
            if (PlayerState.wallruning) { return; }

            _currentSlideTime = 0f;

            playerHeight = 1.5f;

            playerCollider.center = Vector3.up * 0.75f;
            playerCollider.height = playerHeight;

            MoveCamera.Instance.cameraPosition.localPosition = _cameraPositionCrouchPos;
            MoveCamera.Instance.vaultOffset += _cameraPositionBasePos - _cameraPositionCrouchPos;

            if (rb.velocity.magnitude > 6f)
            {
                StartSlide();
            }
            else
            {
                PlayerState.crouching = true;
            }
        }

        private void StartSlide()
        {
            _currentSlideTime = 0f;

            var dir = GetDirection(_iAxis);

            if (onGround)
            {
                rb.AddForce(dir * 40f, ForceMode.Impulse);
            }
            else
            {
                rb.AddForce(dir * 10f, ForceMode.Impulse);
            }

            _slideTilt = 5f;

            OnStartSlide?.Invoke();

            PlayerState.sliding = true;
        }

        public void StopCrouch()
        {
            if (Physics.Raycast(transform.position, Vector3.up, out RaycastHit hit, 2f, groundMask, QueryTriggerInteraction.Ignore))
            {
                return;
            }

            playerHeight = 2f;

            playerCollider.height = playerHeight;
            playerCollider.center = Vector3.up;

            _cameraPosition.localPosition = _cameraPositionBasePos;

            MoveCamera.Instance.vaultOffset += _cameraPositionCrouchPos - _cameraPositionBasePos;

            _slideTilt = 0f;

            PlayerState.sliding = false;
            PlayerState.crouching = false;
        }

        protected override void Look()
        {
            base.Look();

            _yRotation += _iLookDelta.x * _sensivity * Time.smoothDeltaTime;

            AddCameraPitch(_iLookDelta.y * _sensivity * Time.smoothDeltaTime);

            _directionTransform.rotation = Quaternion.Euler(0f, _yRotation, 0f);
            _cameraPosition.localRotation = Quaternion.Euler(_xRotation, _directionTransform.localEulerAngles.y, wallrunTilt + _currentSlideTilt);
        }

        private void Interact(InputAction.CallbackContext context)
        {
            if (Physics.Raycast(_cameraPosition.position, _cameraPosition.transform.forward, out RaycastHit hit, _interactionDistance, _interactionMask, QueryTriggerInteraction.Collide))
            {
                var interactable = hit.transform.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    interactable.Use(this);
                }
            }
        }

        private IEnumerator HoverActors()
        {
            while (true)
            {
                if (Physics.Raycast(_cameraPosition.position, _cameraPosition.forward, out RaycastHit hit, _interactionDistance, _interactionMask, QueryTriggerInteraction.Collide))
                {
                    if (hit.transform.TryGetComponent<IInteractable>(out var interactable))
                    {
                        interactable.OnHover(this);
                    }
                }

                Debug.DrawRay(_cameraPosition.position, _cameraPosition.forward, Color.blue, 0.25f);

                yield return new WaitForSeconds(0.2f);
            }
        }

        public Vector2 GetInputMoveAxis()
        {
            return _iAxis;
        }

        public ControlsAction GetInputClass()
        {
            return _input;
        }

        private void OnDestroy()
        {
            LocalPlayerInstance = null;
        }

        public void TakePointDamage(object sender, int damage, Vector3 dir, object[] info = null)
        {
            
        }

        public void TakeRadialDamage(object sender, int damage, object[] info = null)
        {
            Health -= damage;
            Health = Mathf.Clamp(Health, 0, _maxHealth);

            if (Health == 0)
            {
                TransitionLoad.SwitchToScene(SceneManager.GetActiveScene().name);
            }  

            OnHealthChanged?.Invoke(Health);
        }
    }
}