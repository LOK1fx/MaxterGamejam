using System;
using UnityEngine;
using Photon.Pun;
using LOK1game.Tools;

namespace com.LOK1game.recode.Player
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public abstract class RigidbodyCharacterBase : MonoBehaviour
    {
        public float PlayerHeight = 2f;

        protected float _xRotation;
        protected float _yRotation;
        protected float _cameraMaxAngles = 90f;

        protected Vector3 _cameraPositionBasePos;
        protected Vector3 _cameraPositionCrouchPos;

        [SerializeField] protected Transform _cameraPosition;

        protected virtual void Awake()
        {
            BindInputs();
        }

        protected virtual void BindInputs()
        {
            if (!PlayerInput.initialized)
            {
                PlayerInput.Init();
            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(_cameraPosition.rotation);
            }
            else
            {
                _cameraPosition.rotation = (Quaternion)stream.ReceiveNext();
            }
        }

        public virtual void AddCameraPitch(float value)
        {
            _xRotation -= value;

            _xRotation = Mathf.Clamp(_xRotation, -_cameraMaxAngles, _cameraMaxAngles);
        }

        public Transform GetCameraTransform()
        {
            return _cameraPosition;
        }    

#if UNITY_EDITOR

        [SerializeField] private Mesh _EDITOR_CHARACTER_MESH;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireMesh(_EDITOR_CHARACTER_MESH, transform.position + Vector3.up, transform.rotation, Vector3.one);

            var campos = transform.position + new Vector3(0f, 1.5f, 0f);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(campos, campos + transform.forward * 2f);
        }


#endif
    }
}