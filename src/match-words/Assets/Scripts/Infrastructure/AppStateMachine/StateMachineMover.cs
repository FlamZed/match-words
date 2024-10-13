using Infrastructure.AppStateMachine.Interfaces;

namespace Infrastructure.AppStateMachine
{
    public class StateMachineMover : IStateMachineMover
    {
        private IGameStateMachine _gameStateMachine;

        public void SetStateMachine(IGameStateMachine gameStateMachine) =>
            _gameStateMachine = gameStateMachine;

        public void Enter<TState>() where TState : class, IState =>
            _gameStateMachine.Enter<TState>();

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload> =>
            _gameStateMachine.Enter<TState, TPayload>(payload);
    }
}
