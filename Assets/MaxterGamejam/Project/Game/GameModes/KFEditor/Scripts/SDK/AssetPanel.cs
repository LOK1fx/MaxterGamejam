using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace com.LOK1game.recode
{
    public class AssetPanel : MonoBehaviour
    {
        public Image thumbnail;

        [HideInInspector] public string assetName = "Unknow asset";

        private void Start()
        {
        }
    }
}