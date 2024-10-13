namespace Infrastructure.AppStateMachine.Interfaces
{
    public interface IStateMachineMover
    {
        void SetStateMachine(IGameStateMachine gameStateMachine);
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
    }
}
