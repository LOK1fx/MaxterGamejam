using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace com.LOK1game.recode.UI
{
    /// <summary>
    /// Кнопка пропуска катсцен.
    /// </summary>
    public class HoldKeyButton : MonoBehaviour
    {
        public UnityEvent OnStartHold;
        public UnityEvent OnPerformed;

        [Space]
        [SerializeField] private Image _fillBar;
        [SerializeField] private float _neededHoldTime = 1f;

        private ControlsAction _inputs;
        private CanvasGroup _canvasGroup;

        private bool _holded;
        private float _timer;

        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0f;

            _inputs = new ControlsAction();

            BindInput();
        }

        private void Update()
        {
            if(_holded && _timer <= _neededHoldTime)
            {
                _timer += Time.deltaTime;
            }
            else if(!_holded && _timer > 0)
            {
                _timer -= Time.deltaTime;
            }

            _fillBar.fillAmount = Mathf.Clamp01(_timer);

            if(!_holded)
            {
                _canvasGroup.alpha = _timer;
            }
        }

        private void BindInput()
        {
            _inputs.UI.Skip.started += ctx => StartHold();
            _inputs.UI.Skip.canceled += ctx => StopHold();
            _inputs.UI.Skip.performed += ctx => OnPerformed?.Invoke();

            _inputs.Enable();
        }

        private void StartHold()
        {
            _holded = true;

            OnStartHold?.Invoke();
        }

        private void StopHold()
        {
            _holded = false;
        }
    }
}