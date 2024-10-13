using Infrastructure.AppStateMachine;
using Zenject;

namespace Infrastructure.DIContainer.Extensions
{
    public static class StateMachineBinder
    {
        public static DiContainer BindStateMachine<TStateMachine>(this DiContainer container)
            where TStateMachine : class, IGameStateMachine
        {
            container.BindInterfacesAndSelfTo<TStateMachine>().AsSingle();
            container.BindInterfacesAndSelfTo<StateMachineMover>().AsSingle();

            return container;
        }

        public static DiContainer BindState<TState>(this DiContainer container)
            where TState : class, IState
        {
            container.BindInterfacesAndSelfTo<TState>().AsSingle();
            return container;
        }

        public static DiContainer BindPayloadState<TState, TPayload>(this DiContainer container)
            where TState : class, IPayloadedState<TPayload>
        {
            container.BindInterfacesAndSelfTo<TState>().AsSingle();
            return container;
        }
    }
}
