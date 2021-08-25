using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LOK1game.recode
{
    [CreateAssetMenu(fileName = "new MoveData", menuName = "ReCode/Character/MoveData")]
    public class CharacterMoveData : ScriptableObject
	{
        public float walkGroundAccelerate;
        public float crouchGroundAccelerate;
        public float sprintGoundAccelerate;
        public float airAccelerate;
        public float jumpForce;
    }
}