using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LOK1game.recode
{
    /// <summary>
    /// KFEditor Object. Для сохранения всех объектов в сцене
    /// </summary>
    public class KFEditorObject : MonoBehaviour
    {
        [Serializable]
	    public struct Data
        {
            public string id;

            public Vector3 position;
            public Quaternion rotation;
            public Vector3 scale;
        }

        public Data data;

#if UNITY_EDITOR

        [ContextMenu("Generate a new ID")]
        public void GenerateId() => data.id = Guid.NewGuid().ToString();

#endif
    }
}