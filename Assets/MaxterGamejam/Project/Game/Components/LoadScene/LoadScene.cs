using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LOK1game.MaxterGamejam
{
    public class LoadScene : MonoBehaviour
    {
        public void Load(string name)
        {
            TransitionLoad.SwitchToScene(name);
        }
    }
}