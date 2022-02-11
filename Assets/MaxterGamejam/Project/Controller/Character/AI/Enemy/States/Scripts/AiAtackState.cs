using System;
using System.Collections.Generic;
using UnityEngine;
using com.LOK1game.recode.AI;
using com.LOK1game.recode.Player;

namespace com.LOK1game.MaxterGamejam
{
    public class AiAtackState : IAiState
    {
        private float _timer;
        private Enemy _enemy;

        private bool _spawned;

        public void Enter(AiAgent agent)
        {
            _timer = 0;

            agent.StopMovement();

            agent.GetComponent<Animator>().SetTrigger("Atack");

            _enemy = agent.GetComponent<Enemy>();

            _enemy.AddHealth(80);
        }

        public void Exit(AiAgent agent)
        {
            agent.ResumeMovement();
        }

        public AiStateId GetStateId()
        {
            return AiStateId.Atacking;
        }

        public void Update(AiAgent agent)
        {
            if(_timer >= 2 && !_spawned)
            {
                _enemy.CreateCrystal(Player.LocalPlayerInstance.transform.position);
                _timer = 0;
                _spawned = true;

                agent.StateMachine.SetState(AiStateId.Chase);
            }
            else if(PlayerState.OnGround)
            {
                _timer += Time.deltaTime;
            }
        }
    }
}