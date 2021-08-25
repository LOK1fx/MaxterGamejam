using UnityEngine;

namespace com.LOK1game.recode
{
    /// <summary>
    /// База режима со спавнами
    /// </summary>
    public abstract class GameModeBaseWithSpawns : GameModeBase
    {
        public SpawnPointBase[] spawns;
    }
}
