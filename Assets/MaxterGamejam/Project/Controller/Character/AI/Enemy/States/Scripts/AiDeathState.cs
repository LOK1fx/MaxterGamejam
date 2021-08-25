using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.LOK1game.recode.AI;

namespace com.LOK1game.MaxterGamejam
{
    public class AiDeathState : IAiState
    {
        public void Enter(AiAgent agent)
        {
            agent.GetNavMeshAgent(out var navAgent);

            navAgent.enabled = false;
        }

        public void Exit(AiAgent agent)
        {
        }

        public AiStateId GetStateId()
        {
            return AiStateId.Death;
        }

        public void Update(AiAgent agent)
        {
        }
    }
}