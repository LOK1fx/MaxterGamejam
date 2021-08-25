using UnityEngine;

namespace com.LOK1game.recode
{
    public static class Match
    {
        public static T GetController<T>() where T : GameControllerBase
        {
            return (T)GameObject.FindObjectOfType<T>();
        }
    }
}