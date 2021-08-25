using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LOK1game.recode  //LOK1game script behaviour😎💗
{
    [CreateAssetMenu(fileName = "new GameMode Data", menuName = "ReCode/GameModes/Map/Mode")]
	public abstract class GameModeDataBase : ScriptableObject
	{
        public GameObject hud;
        public GameObject player;
        public GameObject playerController;
        public GameObject playerCamera;
        public GameObject spectator;
    }
}