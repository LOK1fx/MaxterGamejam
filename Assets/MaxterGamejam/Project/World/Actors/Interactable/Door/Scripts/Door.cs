using System;
using UnityEngine;
using UnityEngine.Events;

namespace com.LOK1game.recode.World
{
    public class Door : InteractableActor
    {
        #region Events

        public UnityEvent UnityOnOpen;
        public UnityEvent UnityOnClose;
        public UnityEvent UnityOnHover;

        public event Action OnOpen;
        public event Action OnClose;

        #endregion

        [SerializeField] private Outline _outline;

        private bool _opened;

        private void Start()
        {
            _outline.enabled = false;
        }

        public override void Use(object sender)
        {
            if(_opened)
            {
                Close();
            }
            else
            {
                Open();
            }
        }

        public override void OnHover(object sender)
        {
            if(!isHovered)
            {
                UnityOnHover?.Invoke();
            }

            isHovered = true;

            _outline.enabled = isHovered;
        }

        private void Open()
        {
            _opened = true;

            OnOpen?.Invoke();
            UnityOnOpen?.Invoke();
        }

        private void Close()
        {
            _opened = false;

            OnClose?.Invoke();
            UnityOnClose?.Invoke();
        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            if(TryGetComponent<BoxCollider>(out var collider))
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(transform.position + collider.center, collider.size);
            }
        }

#endif
    }
}