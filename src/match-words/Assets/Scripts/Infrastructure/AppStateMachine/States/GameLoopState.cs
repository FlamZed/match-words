using System.Threading;
using Cysharp.Threading.Tasks;
using Feature.PrintMachine;
using Feature.Wallet;
using Infrastructure.AppStateMachine.Interfaces;
using Infrastructure.Data.Type;
using Infrastructure.GameManager;
using Infrastructure.Progress;
using UnityEngine;

namespace Infrastructure.AppStateMachine.States
{
    public class GameLoopState : IPayloadedState<string>
    {
        private readonly IStateMachineMover _stateMachineMover;
        private readonly IGameManager _gameManager;
        private readonly IGameProgressService _progressService;
        private readonly IWalletService _walletService;

        private string _levelName;

        private PrintMachineController _printManager;

        private CancellationTokenSource _cancellationToken;

        public GameLoopState(
            IStateMachineMover stateMachineMover,
            IGameManager gameManager,
            IGameProgressService progressService,
            IWalletService walletService)
        {
            _walletService = walletService;
            _progressService = progressService;
            _gameManager = gameManager;
            _stateMachineMover = stateMachineMover;
        }

        public async void Enter(string levelName)
        {
            _cancellationToken = new CancellationTokenSource();

            _levelName = levelName;

            _printManager = await _gameManager.AwaitServiceLoading<PrintMachineController>();

            _printManager.OnWordCompleted += OnWordCompleted;
            _printManager.OnLeaveButtonPressed += LoadLevelScene;

            var currentProgress = _progressService.GetProgress();

            var levelSeconds = currentProgress.WordsProgress[_levelName].LevelSeconds;
            _printManager.UpdateTimer(levelSeconds);

            SetWordsPregress();

            StartLevelTimer(levelSeconds, _cancellationToken.Token).Forget();
        }

        public void Exit() =>
            _cancellationToken.Cancel();

        private void LoadLevelScene() =>
            _stateMachineMover.Enter<LoadMenuState>();

        private void OnWordCompleted(DictionaryEntry entry)
        {
            _walletService.AddCoins(10); // TODO: Add coins count to config
            _progressService.UpdateProgress(_levelName, entry.Word);

            SetWordsPregress();

            _progressService.SaveProgress();
        }

        private void SetWordsPregress()
        {
            var currentProgress = _progressService.GetProgress();

            var totalWords = currentProgress.WordsProgress[_levelName].TotalWords.Count;
            var unlockedWords = currentProgress.WordsProgress[_levelName].CompletedWords.Count;

            _printManager.UpdateWordCounter(totalWords, unlockedWords);
        }

        private async UniTask StartLevelTimer(int initTime, CancellationToken cancellationToken)
        {
            var time = initTime;

            while (!cancellationToken.IsCancellationRequested)
            {
                time += 1;

                _printManager.UpdateTimer(time);
                _progressService.UpdateLevelTime(_levelName, time);

                await UniTask.Delay(1000, cancellationToken: cancellationToken);
            }

            Debug.LogWarning("Timer stopped");
        }
    }
}
