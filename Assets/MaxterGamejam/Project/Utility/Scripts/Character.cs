using UnityEngine;

namespace LOK1game.Tools
{
    public class CharacterMath
    {
        public struct MoveParams
        {
            public MoveParams(Vector3 accelDir, Vector3 prevVel)
            {
                accelerationDirection = accelDir;
                previousVelocity = prevVel;
            }

            public Vector3 accelerationDirection;
            public Vector3 previousVelocity;
        }

        public static Vector3 Project(Vector3 dir, Vector3 normal)
        {
            return dir - Vector3.Dot(dir, normal) * normal;
        }

        public static bool IsFloor(Vector3 normal)
        {
            return Vector3.Angle(Vector3.up, normal) < 45;
        }

        public static Vector3 Accelerate(MoveParams moveParams, float accelerate, float maxVelocity, float delta)
        {
            float projectVel = Vector3.Dot(moveParams.previousVelocity, moveParams.accelerationDirection);
            float accelerationVelocity = accelerate * delta;

            if (projectVel + accelerationVelocity > maxVelocity)
                accelerationVelocity = maxVelocity - projectVel;

            return moveParams.previousVelocity + moveParams.accelerationDirection * accelerationVelocity;
        }
    }
}