using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

namespace com.LOK1game.recode.UI
{
    public class IntroVideosOrder : MonoBehaviour
    {
        public UnityEvent OnAllVideosPlayed;

        [SerializeField] private VideoClip[] _clips;

        private VideoPlayer _videoPlayer;
        private int _currentVideoIndex;

        private void Awake() => _videoPlayer = GetComponent<VideoPlayer>();

        private void Start()
        {
            _videoPlayer.clip = _clips[_currentVideoIndex];
            _videoPlayer.loopPointReached += PlayNext;
        }

        private void PlayNext(VideoPlayer source)
        {
            _currentVideoIndex++;

            if(_currentVideoIndex + 1 > _clips.Length)
            {
                OnAllVideosPlayed?.Invoke();

                return;
            }

            source.clip = _clips[_currentVideoIndex];
            source.Play();
        }

        private void OnDisable()
        {
            OnAllVideosPlayed?.Invoke();
        }
    }

}