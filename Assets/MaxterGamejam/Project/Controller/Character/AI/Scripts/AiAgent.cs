using UnityEngine;
using UnityEngine.AI;
using com.LOK1game.MaxterGamejam;

namespace com.LOK1game.recode.AI
{
    public class AiAgent : MonoBehaviour
    {
        public AiStateMachine StateMachine;

        [HideInInspector] public Transform Target;

        private float _defaultSpeed;

        [SerializeField] private AiStateId _startState;

        private NavMeshAgent _navAgent;

        private void Awake()
        {
            _navAgent = GetComponent<NavMeshAgent>();

            StateMachine = new AiStateMachine(this);
            StateMachine.AddState(new AiChaseState());
            StateMachine.AddState(new AiDeathState());
            StateMachine.AddState(new AiIdleState());
            StateMachine.AddState(new AiAtackState());
            StateMachine.SetState(_startState);

            _defaultSpeed = _navAgent.speed;
        }

        private void Update()
        {
            StateMachine.Update();
        }

        public void Death()
        {
            StateMachine.SetState(AiStateId.Death);
        }

        public void StopMovement()
        {
            _navAgent.speed = 0;
        }

        public void ResumeMovement()
        {
            _navAgent.speed = _defaultSpeed;
        }

        public void GetNavMeshAgent(out NavMeshAgent navAgent)
        {
            navAgent = _navAgent;
        }
    }
}