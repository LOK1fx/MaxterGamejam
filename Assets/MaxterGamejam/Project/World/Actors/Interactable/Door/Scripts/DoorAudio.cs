using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LOK1game.recode.World
{
    public class DoorAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource _audio;

        [Space]
        [SerializeField] private AudioClip[] _closeDoorClips;
        [SerializeField] private AudioClip[] _openDoorClips;

        private Door _door;

        private void Awake() => _door = GetComponent<Door>();

        private void Start()
        {
            _door.OnOpen += OnDoorOpen;
            _door.OnClose += OnDoorClose;
        }

        private void OnDoorClose()
        {
            _audio.PlayOneShot(GetRandomClip(_closeDoorClips));
        }

        private void OnDoorOpen()
        {
            _audio.PlayOneShot(GetRandomClip(_openDoorClips));
        }

        private AudioClip GetRandomClip(AudioClip[] clips)
        {
            return clips[Random.Range(0, clips.Length)];
        }

        private void OnDestroy()
        {
            _door.OnOpen -= OnDoorOpen;
            _door.OnClose -= OnDoorClose;
        }
    }
}