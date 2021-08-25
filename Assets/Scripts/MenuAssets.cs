using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game
{
    [CreateAssetMenu]
    public class MenuAssets : ScriptableObject
    {
        public Sprite Background;

        [Header("Colors")]
        public Color PrimaryColor = Color.white;
        public Color SecondaryColor = Color.white;
    }
}