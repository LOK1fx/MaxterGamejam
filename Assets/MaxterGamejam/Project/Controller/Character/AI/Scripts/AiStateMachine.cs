using System;

namespace com.LOK1game.recode.AI
{
    public class AiStateMachine
    {
        private IAiState[] _states;
        private AiStateId _currentState;
        private AiAgent _agent;

        public AiStateMachine(AiAgent agent)
        {
            _agent = agent;

            var length = Enum.GetNames(typeof(AiStateId)).Length;

            _states = new IAiState[length];
        }

        public void Update()
        {
            GetState(_currentState)?.Update(_agent);
        }

        public void AddState(IAiState state)
        {
            var index = (int)state.GetStateId();

            _states[index] = state;
        }

        public void SetState(AiStateId newStateId)
        {
            GetState(_currentState)?.Exit(_agent);

            _currentState = newStateId;

            GetState(newStateId)?.Enter(_agent);
        }

        public IAiState GetState(AiStateId stateId)
        {
            var index = (int)stateId;

            return _states[index];
        }
    }
}