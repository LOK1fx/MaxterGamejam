using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LOK1game.MaxterGamejam
{
    [RequireComponent(typeof(Animator), typeof(AudioSource))]
    public class Hitmarker : MonoBehaviour
    {
        private struct Param
        {
            public AudioClip AudioClip { get; private set; }
            public string AnimName { get; private set; }
            public float AudioPitch { get; private set; }

            public Param(AudioClip clip, string anim)
            {
                AudioClip = clip;
                AnimName = anim;
                AudioPitch = 1f;
            }

            public Param(AudioClip clip, string anim, float pitch)
            {
                AudioClip = clip;
                AnimName = anim;
                AudioPitch = pitch;
            }
        }

        public static Hitmarker Instance { get; set; }

        [SerializeField] private AudioClip _bodyHit;
        [SerializeField] private AudioClip _headHit;
        [SerializeField] private AudioClip _getKill;

        private Animator _animator;
        private AudioSource _audio;

        private const string HEAD_CRACK_ANIM = "HeadCrack";
        private const string BODY_HIT_ANIM = "BodyHit";
        private const string GET_KILL_ANIM = "GetKill";

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }

            _audio = GetComponent<AudioSource>();
            _animator = GetComponent<Animator>();

            gameObject.SetActive(false);
        }

        public void HeadCrack()
        {
            Hitmark(new Param(_headHit, HEAD_CRACK_ANIM));
        }

        public void BodyHit()
        {
            var pitch = Random.Range(0.9f, 1.1f);

            Hitmark(new Param(_bodyHit, BODY_HIT_ANIM, pitch));
        }

        public void GetKill()
        {
            var pitch = Random.Range(0.9f, 1.1f);

            Hitmark(new Param(_getKill, GET_KILL_ANIM, pitch));
        }

        private void Hitmark(Param param)
        {
            StopAnimation();

            _animator.Play(param.AnimName, 0, 0);

            _audio.pitch = param.AudioPitch;
            _audio.PlayOneShot(param.AudioClip);

            gameObject.SetActive(true);
        }

        private void StopAnimation()
        {
            _animator.enabled = false;
            _animator.enabled = true;
        }
    }
}