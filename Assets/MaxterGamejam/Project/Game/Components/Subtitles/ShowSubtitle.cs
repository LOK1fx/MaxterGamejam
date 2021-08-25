using UnityEngine;
using com.LOK1game.recode;

namespace com.LOK1game.MaxterGamejam
{
    public class ShowSubtitle : MonoBehaviour
    {
        [SerializeField] private SubtitleParam _subtitle;

        public void Show()
        {
            Subtitles.Subtitle(_subtitle.TextKey, _subtitle.SpeakerKey, _subtitle.ShowTime);
        }
    }
}