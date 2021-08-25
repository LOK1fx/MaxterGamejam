using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LOK1game.recode.Player
{
    [RequireComponent(typeof(CharacterController))]
    public abstract class CharacterControllerCharacterBase : MonoBehaviour
    {
        [SerializeField] protected CharacterMoveData _moveData;

        protected CharacterController controller;

        protected virtual void Awake()
        {
            InitializeInput();

            controller = GetComponent<CharacterController>();
        }

        protected virtual void InitializeInput()
        {

        }

        protected virtual void Update()
        {
            Movement();
            Look();
        }

        protected virtual void Movement()
        {

        }

        protected virtual void Look()
        {

        }

        protected virtual void Jump()
        {

        }

#if UNITY_EDITOR

        [SerializeField] private Mesh _EDITOR_CHARACTER_MESH;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            //Gizmos.DrawWireCube(transform.position + new Vector3(0f, 1f, 0f), new Vector3(1f, 2f, 1f));
            Gizmos.DrawWireMesh(_EDITOR_CHARACTER_MESH, transform.position + Vector3.up, transform.rotation, Vector3.one);

            var campos = transform.position + new Vector3(0f, 1.5f, 0f);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(campos, campos + transform.forward * 2f);
        }

#endif
    }
}