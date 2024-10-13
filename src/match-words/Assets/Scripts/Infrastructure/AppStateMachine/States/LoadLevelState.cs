using Feature.LoadingCurtain;

namespace Infrastructure.AppStateMachine.States
{
    public class LoadLevelState : IState
    {
        private const string SceneName = "LevelMenu";

        private readonly IStateMachineMover _stateMachineMover;
        private readonly ISceneLoader _sceneLoader;
        private readonly ILoadingCurtain _curtain;

        public LoadLevelState(
            IStateMachineMover stateMachineMover,
            ISceneLoader sceneLoader,
            ILoadingCurtain curtain)
        {
            _stateMachineMover = stateMachineMover;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
        }

        public void Enter()
        {
            _curtain.Show();

            _sceneLoader.Load(SceneName, onLoaded: OnLoaded);
        }

        private void OnLoaded() => _stateMachineMover.Enter<GameLoopState>();

        public void Exit() => _curtain.Hide();
    }
}
