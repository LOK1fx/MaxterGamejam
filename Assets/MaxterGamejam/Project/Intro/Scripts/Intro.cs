using UnityEngine;
using UnityEngine.SceneManagement;
using LOK1game.Tools;

namespace com.LOK1game.recode.UI
{
    public class Intro : MonoBehaviour
    {
        public void StartPlay()
        {
            SceneManager.LoadScene(1);
        }
    }
}