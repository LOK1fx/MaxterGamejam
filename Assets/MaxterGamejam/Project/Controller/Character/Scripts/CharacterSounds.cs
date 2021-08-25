using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LOK1game.recode.Player
{
    [RequireComponent(typeof(AudioSource))]
    public class CharacterSounds : MonoBehaviour
    {
        protected AudioSource audio;
        
        protected virtual void Start()
        {
            audio = GetComponent<AudioSource>();
        }
    }
}