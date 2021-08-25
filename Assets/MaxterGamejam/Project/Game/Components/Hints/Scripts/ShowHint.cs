using UnityEngine;

namespace com.LOK1game.MaxterGamejam
{
    public class ShowHint : MonoBehaviour
    {
        [SerializeField] private Hint _hint;

        public void Show()
        {
            HintController.ShowHint(_hint);
        }
    }
}