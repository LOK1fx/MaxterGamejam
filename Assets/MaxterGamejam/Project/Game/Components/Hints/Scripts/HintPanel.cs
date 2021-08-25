using UnityEngine;
using TMPro;

namespace com.LOK1game.MaxterGamejam
{
    public class HintPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _description;

        private float _currentShowTimer = 1f;
        private bool _showed = false;

        private Animator _animator;

        private const string ANIM_OPEN_NAME = "Open";
        private const string ANIM_CLOSE_NAME = "Close";

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetHintInfo(Hint hint)
        {
            _title.text = $"¿{hint.Title}?";
            _description.text = hint.Description;
            _currentShowTimer = hint.ShowTime;

            ShowHint();
        }

        private void Update()
        {
            _currentShowTimer -= Time.deltaTime;

            if(_currentShowTimer <= 0)
            {
                RemoveHint();
            }
        }

        private void ShowHint()
        {
            _animator.SetTrigger(ANIM_OPEN_NAME);
        }

        private void RemoveHint()
        {
            if(_showed) { return; }

            _showed = true;

            _animator.SetTrigger(ANIM_CLOSE_NAME);

            Destroy(gameObject, 0.3f);
        }
    }
}