using System;
using Feature.LevelMenu;
using Infrastructure.AppStateMachine.Interfaces;
using Infrastructure.GameManager;

namespace Infrastructure.AppStateMachine.States
{
    public class MainMenuState : IState
    {
        private readonly IStateMachineMover _stateMachineMover;
        private readonly IGameManager _gameManager;

        public MainMenuState(
            IStateMachineMover stateMachineMover,
            IGameManager gameManager)
        {
            _gameManager = gameManager;
            _stateMachineMover = stateMachineMover;
        }

        public async void Enter()
        {
            var levelController = await _gameManager.AwaitServiceLoading<LevelMenuController>();

            levelController.Initialize();

            var selectedLevelName = await levelController.LoadLevelCompletionSource.Task;

            if (string.IsNullOrEmpty(selectedLevelName))
                throw new ArgumentException("Selected level name is null or empty");

            _stateMachineMover.Enter<LoadLevelState, string>(selectedLevelName);
        }

        public void Exit() { }
    }
}
