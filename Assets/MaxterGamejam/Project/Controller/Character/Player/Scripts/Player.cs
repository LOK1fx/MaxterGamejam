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

        [Space]
        private ControlsAction _input;
        private Vector2 _iAxis;
        private Vector3 _iLookDelta;

        public PlayerMovement PlayerMovement { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            Health = _maxHealth;

            PlayerMovement = GetComponent<PlayerMovement>();

            LocalPlayerInstance = this;
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            StartCoroutine(HoverActors());

            _cameraPositionBasePos = _cameraPosition.localPosition;
            _cameraPositionCrouchPos = _cameraPositionBasePos + Vector3.down * 0.5f;

            PlayerMovement.OnStartCrouch += OnStartCrouch;
            PlayerMovement.OnStartSlide += OnStartSlide;
            PlayerMovement.OnStopCrouch += OnStopCrouch;
        }

        private void Update()
        {
            //need to CHANGE
            if (Input.GetKeyDown(KeyCode.Plus))
            {
                TransitionLoad.SwitchToScene("Menu");
            }

            UpdateFX();
            PlayerMovement.SetAxisInput(_iAxis);
        }

        private void FixedUpdate()
        {
            PlayerMovement.Move();
        }

        private void LateUpdate()
        {
            Look();
        }

        private void OnStopCrouch()
        {
            _cameraPosition.localPosition = _cameraPositionBasePos;

            MoveCamera.Instance.vaultOffset += _cameraPositionCrouchPos - _cameraPositionBasePos;

            _slideTilt = 0f;
        }

        private void OnStartSlide()
        {
            _slideTilt = 5f;
        }

        private void OnStartCrouch()
        {
            MoveCamera.Instance.cameraPosition.localPosition = _cameraPositionCrouchPos;
            MoveCamera.Instance.vaultOffset += _cameraPositionBasePos - _cameraPositionCrouchPos;
        }

        protected override void BindInputs()
        {
            base.BindInputs();

            _input = PlayerInput.GetInput();

            _input.Player.Move.performed += ctx => _iAxis = ctx.ReadValue<Vector2>();
            _input.Player.Look.performed += ctx => _iLookDelta = ctx.ReadValue<Vector2>();
            _input.Player.Fire.performed += ctx => AddCameraPitch(1f);
            _input.Player.Jump.performed += ctx => PlayerMovement.Jump();

            _input.Player.Interact.performed += Interact;

            _input.Player.Crouch.started += ctx => PlayerMovement.StartCrouch();
            _input.Player.Crouch.canceled += ctx => PlayerMovement.StopCrouch();

            _input.Player.Sprint.performed += ctx => PlayerState.sprinting = !PlayerState.sprinting;

            _sensivity = Settings.GetSensivity();
        }
        

        private void UpdateFX()
        {
            _currentSlideTilt = Mathf.Lerp(_currentSlideTilt, _slideTilt, Time.deltaTime * _slideTiltSmooth);
        }

        private void Look()
        {
            _yRotation += _iLookDelta.x * _sensivity * Time.smoothDeltaTime;

            AddCameraPitch(_iLookDelta.y * _sensivity * Time.smoothDeltaTime);

            var directionTransform = PlayerMovement.GetDirectionTransform();

            directionTransform.rotation = Quaternion.Euler(0f, _yRotation, 0f);
            _cameraPosition.localRotation = Quaternion.Euler(_xRotation, directionTransform.localEulerAngles.y, wallrunTilt + _currentSlideTilt);
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