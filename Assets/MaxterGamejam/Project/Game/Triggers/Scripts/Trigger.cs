using UnityEngine;
using UnityEngine.Events;

namespace com.LOK1game.recode
{
    public class Trigger : MonoBehaviour
    {
        [SerializeField] private bool _triggerOnce;
        [SerializeField] private bool _activateOnEnable = true;

        [Space]
        public UnityEvent OnTriggerEnterEvent;
        public UnityEvent OnTriggerExitEvent;

        private bool activated;

        private void Start()
        {
            activated = !_activateOnEnable;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(EnterCondition(other))
            {
                OnTriggerEnterEvent?.Invoke();

                if (_triggerOnce)
                {
                    activated = true;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OnTriggerExitEvent?.Invoke();
            }
        }

        protected virtual bool EnterCondition(Collider other)
        {
            if(activated)
            { 
                return false; 
            }
            if(other.CompareTag("Player"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}