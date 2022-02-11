using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace com.LOK1game.recode.AI
{
    public class AiChaseState : IAiState
    {
        private NavMeshAgent _navAgent;

        private readonly float _maxTime = 0.6f;
        private readonly float _maxDistance = 1f;

        private float _timer;

        public void Enter(AiAgent agent)
        {
            agent.GetNavMeshAgent(out _navAgent);
        }

        public void Exit(AiAgent agent)
        {
            agent.Target = null;
        }

        public void Update(AiAgent agent)
        {
            if(Player.Player.LocalPlayerInstance == null) { return; }

            _timer -= Time.deltaTime;

            if(_timer < 0f)
            {
                agent.Target = Player.Player.LocalPlayerInstance.transform;

                var distance = (agent.Target.position - _navAgent.destination).sqrMagnitude;

                if(distance > _maxDistance * _maxDistance)
                {
                    _navAgent.destination = agent.Target.position;

                    if(distance < 8*8)
                    {
                        agent.StateMachine.SetState(AiStateId.Atacking);
                    }
                }

                _timer = _maxTime;
            }
        }

        public AiStateId GetStateId()
        {
            return AiStateId.Chase;
        }
    }
}