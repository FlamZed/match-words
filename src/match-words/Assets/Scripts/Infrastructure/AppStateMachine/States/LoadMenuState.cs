using Feature.LoadingCurtain;
using Infrastructure.AppStateMachine.Interfaces;

namespace Infrastructure.AppStateMachine.States
{
    public class LoadMenuState : IState
    {
        private const string SceneName = "LevelMenu";

        private readonly IStateMachineMover _stateMachineMover;
        private readonly ISceneLoader _sceneLoader;
        private readonly ILoadingCurtain _curtain;

        public LoadMenuState(
            IStateMachineMover stateMachineMover,
            ISceneLoader sceneLoader,
            ILoadingCurtain curtain)
        {
            _curtain = curtain;
            _sceneLoader = sceneLoader;
            _stateMachineMover = stateMachineMover;
        }

        public void Enter()
        {
            _curtain.Show();
            _sceneLoader.Load(SceneName, onLoaded: OnLoaded);
        }

        private void OnLoaded() => _stateMachineMover.Enter<MainMenuState>();

        public void Exit() => _curtain.Hide();
    }
}
