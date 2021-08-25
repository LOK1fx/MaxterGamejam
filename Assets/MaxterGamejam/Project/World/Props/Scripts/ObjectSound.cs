using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LOK1game.recode
{
    [RequireComponent(typeof(AudioSource), typeof(Rigidbody))]
    public class ObjectSound : MonoBehaviour
    {
        public AudioClip[] clips;

        private AudioSource _audio;
        private Rigidbody _rb;

        private float _volume;

        private void Start()
        {
            _audio = GetComponent<AudioSource>();
            _rb = GetComponent<Rigidbody>();

            _volume = _audio.volume;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(_rb.velocity.magnitude > 1.8f)
            {
                var clip = Random.Range(0, clips.Length);
                var volume = Random.Range(_volume - 0.15f, _volume);

                _audio.volume = volume;
                _audio.PlayOneShot(clips[clip]);
            }
        }
    }
}