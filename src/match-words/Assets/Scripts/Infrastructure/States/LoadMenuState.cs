using Logic.Loading;
using Zenject;

namespace Infrastructure.States
{
    public class LoadMenuState : IState
    {
        private const string MenuScene = "LevelMenu";

        private readonly GameStateMachine _stateMachine;

        private readonly ISceneLoader _sceneLoader;
        private readonly ILoadingCurtain _curtain;

        public LoadMenuState(GameStateMachine stateMachine, DiContainer diContainer)
        {
            _stateMachine = stateMachine;

            _sceneLoader = diContainer.Resolve<ISceneLoader>();
            _curtain = diContainer.Resolve<ILoadingCurtain>();
        }

        public void Exit() =>
            _curtain.Hide();

        public void Enter() =>
            _sceneLoader.Load(MenuScene, onLoaded: OnLoaded);

        private void OnLoaded() =>
            _stateMachine.Enter<MainMenuState>();
    }
}
