using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.AppStateMachine.Interfaces;
using Zenject;

namespace Infrastructure.AppStateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        private readonly DiContainer _diContainer;

        public GameStateMachine(IStateMachineMover stateMachineMover, IEnumerable<IExitableState> states)
        {
            stateMachineMover.SetStateMachine(this);
            _states = states.ToDictionary(x => x.GetType(), x => x);
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}
