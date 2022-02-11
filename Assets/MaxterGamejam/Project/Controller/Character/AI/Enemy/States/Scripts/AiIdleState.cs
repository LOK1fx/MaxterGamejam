using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.LOK1game.recode.Player;
using com.LOK1game.recode.AI;
using UnityEngine.AI;

namespace com.LOK1game.MaxterGamejam
{
    public class AiIdleState : IAiState
    {
        private NavMeshAgent _navAgent;

        public void Enter(AiAgent agent)
        {
            agent.GetNavMeshAgent(out _navAgent);
        }

        public void Exit(AiAgent agent)
        {
        }

        public AiStateId GetStateId()
        {
            return AiStateId.Idle;
        }

        public void Update(AiAgent agent)
        {
            if(Player.LocalPlayerInstance != null)
            {
                agent.Target = Player.LocalPlayerInstance.transform;
            }

            var direction = agent.Target.position - agent.transform.position;

            if(direction.magnitude > 15f)
            {
                return;
            }

            var agentDir = agent.transform.forward;

            direction.Normalize();

            var dot = Vector3.Dot(direction, agentDir);

            if(dot > 0f)
            {
                agent.StateMachine.SetState(AiStateId.Chase);
            }
        }
    }
}