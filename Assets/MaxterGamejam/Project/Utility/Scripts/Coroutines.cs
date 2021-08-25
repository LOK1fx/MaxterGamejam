using UnityEngine;
using System.Collections;

namespace LOK1game.Tools
{
    /// <summary>
    /// Позволяет использовать короутины в не MonoBehaviour скрипте
    /// </summary>
    public sealed class Coroutines : MonoBehaviour
    {
        private static Coroutines _instance
        {
            get
            {
                if(m_instance == null)
                {
                    var go = new GameObject("[COROUTINE MANAGER]");
                    m_instance = go.AddComponent<Coroutines>();
                    DontDestroyOnLoad(go);
                }

                return m_instance;
            }
        }

        private static Coroutines m_instance;

        public static Coroutine StartRoutine(IEnumerator enumerator)
        {
            return _instance.StartCoroutine(enumerator);
        }

        public static void StopRoutine(Coroutine coroutine)
        {
            _instance.StopCoroutine(coroutine);
        }
    }
}