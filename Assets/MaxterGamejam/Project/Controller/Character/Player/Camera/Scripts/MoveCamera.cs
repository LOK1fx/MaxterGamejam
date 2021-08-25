using UnityEngine;
using com.LOK1game.MaxterGamejam;

namespace com.LOK1game.recode.Player
{
    /// <summary>
    /// Месиво ебанное.
    /// </summary>
    public class MoveCamera : MonoBehaviour
    {
        public static MoveCamera Instance { get; set; }

        [Tooltip("'Коллизия' камеры")]
        public bool physicsCamera;

        [SerializeField] private LayerMask _physicsCameraMask;

        [Space]
        public Vector3 fixedOffset;
        public Vector3 fixedCameraOffset;

        [HideInInspector] public Vector3 lerpOffset;
        [HideInInspector] public Vector3 vaultOffset;
        [HideInInspector] public Vector3 lerpCameraOffset;
        [HideInInspector] public Vector3 vaultCameraOffset;

        private Vector3 _collisionOffset;

        [Space]
        [HideInInspector] public Camera currentCamera;
        public Transform cameraPosition;

        [Header("Говно код из за недостатка времент и сил")]
        [SerializeField] private ItemsBobbing _items;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start() => currentCamera = Camera.main;

        private void Update()
        {
            if(cameraPosition == null) { return; }

            vaultOffset = Vector3.Lerp(vaultOffset, lerpOffset, Time.smoothDeltaTime * 6f);
            lerpOffset = Vector3.Lerp(lerpOffset, Vector3.zero, Time.smoothDeltaTime * 6f);

            vaultCameraOffset = Vector3.Lerp(vaultCameraOffset, lerpCameraOffset, Time.smoothDeltaTime * 6f);
            lerpCameraOffset = Vector3.Lerp(lerpCameraOffset, Vector3.zero, Time.smoothDeltaTime * 6f);

            if(physicsCamera)
            {
                PhysicsChecks();
            }

            transform.position = cameraPosition.position + vaultOffset + fixedOffset;
            transform.rotation = cameraPosition.rotation;

            currentCamera.transform.localPosition = vaultCameraOffset + fixedCameraOffset + _collisionOffset;

            _items.TargetPos = vaultOffset * 0.35f;
        }

        private void PhysicsChecks()
        {
            if(Physics.Linecast(cameraPosition.position, currentCamera.transform.position, out RaycastHit hit, _physicsCameraMask, QueryTriggerInteraction.Ignore))
            {
                _collisionOffset = currentCamera.transform.forward * -Vector3.Distance(transform.position, hit.point) - fixedCameraOffset;
            }
            else
            {
                _collisionOffset = Vector3.zero;
            }
        }
    }
}