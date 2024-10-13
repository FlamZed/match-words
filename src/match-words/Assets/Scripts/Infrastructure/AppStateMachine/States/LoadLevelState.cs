using Feature.LoadingCurtain;
using Feature.PrintMachine;
using Infrastructure.AppStateMachine.Interfaces;
using Infrastructure.GameManager;
using Infrastructure.Progress;
using Infrastructure.Services.Audio;
using Infrastructure.Services.Audio.Type;

namespace Infrastructure.AppStateMachine.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string SceneName = "Game";

        private readonly IStateMachineMover _stateMachineMover;
        private readonly ISceneLoader _sceneLoader;
        private readonly ILoadingCurtain _curtain;
        private readonly IGameManager _gameManager;
        private readonly IGameProgressService _progressService;
        private readonly IAudioService _audioService;

        private string _levelName;

        public LoadLevelState(
            IStateMachineMover stateMachineMover,
            ISceneLoader sceneLoader,
            ILoadingCurtain curtain,
            IGameManager gameManager,
            IGameProgressService progressService,
            IAudioService audioService)
        {
            _audioService = audioService;
            _progressService = progressService;
            _gameManager = gameManager;
            _stateMachineMover = stateMachineMover;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
        }

        public void Enter(string levelName)
        {
            _levelName = levelName;
            _curtain.Show();

            _audioService.PlayBackground(BackgroundClip.Game);

            _sceneLoader.Load(SceneName, onLoaded: OnLoaded);
        }

        private async void OnLoaded()
        {
            var printManager = await _gameManager.AwaitServiceLoading<PrintMachineController>();

            var progressLevel = _progressService.GetProgress();
            var selectedLevel = progressLevel.WordsProgress[_levelName];

            printManager.Initialize(_levelName, selectedLevel);

            _stateMachineMover.Enter<GameLoopState, string>(_levelName);
        }

        public void Exit() => _curtain.Hide();
    }
}
