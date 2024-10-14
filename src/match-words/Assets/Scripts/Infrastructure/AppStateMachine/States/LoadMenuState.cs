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

        public async void Enter()
        {
            _curtain.Show();

            await _sceneLoader.LoadSceneAsync(SceneName);

            _audioService.PlayBackground(BackgroundClip.Menu);

            _stateMachineMover.Enter<MainMenuState>();
        }

        public void Exit() =>
            _curtain.Hide();
    }
}
