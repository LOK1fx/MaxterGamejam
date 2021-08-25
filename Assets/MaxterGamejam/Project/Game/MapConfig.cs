using UnityEngine;
using System.Collections;

namespace com.LOK1game.recode
{
    public class MapConfig : MonoBehaviour
    {
        /// <summary>
        /// Загружает конфиг для карты из ресурсов
        /// </summary>
        /// <typeparam name="T">GameModeDataBase</typeparam>
        /// <param name="sceneName"></param>
        /// <returns></returns>
        public static T LoadConfig<T>(string sceneName) where T : GameModeDataBase
        {
            return (T)Resources.Load($"Game/GameModes/Data/{sceneName}GameModeData");
        }
    }
}