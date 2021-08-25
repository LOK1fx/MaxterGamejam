using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LOK1game.recode
{
    [CreateAssetMenu(fileName = "new MoveData", menuName = "ReCode/Player/MoveData")]
    public class PlayerMoveData : CharacterMoveData
    {
        public float friction;
        public float walkGroundMaxVelocity;
        public float crouchGroundMaxVelocity;
        public float sprintGoundMaxVelocity;
        public float airMaxVelocity;
    }
}