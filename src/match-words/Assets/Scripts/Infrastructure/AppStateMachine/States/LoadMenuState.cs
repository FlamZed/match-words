using Feature.LoadingCurtain;
using Infrastructure.AppStateMachine.Interfaces;
using Infrastructure.Services.Audio;
using Infrastructure.Services.Audio.Type;

namespace Infrastructure.AppStateMachine.States
{
    public class LoadMenuState : IState
    {
        private const string SceneName = "LevelMenu";

        private readonly IStateMachineMover _stateMachineMover;
        private readonly ISceneLoader _sceneLoader;
        private readonly ILoadingCurtain _curtain;
        private readonly IAudioService _audioService;

        public LoadMenuState(
            IStateMachineMover stateMachineMover,
            ISceneLoader sceneLoader,
            ILoadingCurtain curtain,
            IAudioService audioService)
        {
            _audioService = audioService;
            _curtain = curtain;
            _sceneLoader = sceneLoader;
            _stateMachineMover = stateMachineMover;
        }

        public void Enter()
        {
            _curtain.Show();
            _sceneLoader.Load(SceneName, onLoaded: OnLoaded);

            _audioService.PlayBackground(BackgroundClip.Menu);
        }

        private void OnLoaded() => _stateMachineMover.Enter<MainMenuState>();

        public void Exit() => _curtain.Hide();
    }
}
