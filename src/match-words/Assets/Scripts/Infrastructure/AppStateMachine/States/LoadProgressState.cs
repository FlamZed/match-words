using Infrastructure.Data;
using Infrastructure.Progress;
using UnityEngine;

namespace Infrastructure.AppStateMachine.States
{
    public class LoadProgressState : IState
    {
        private readonly IStateMachineMover _gameStateMachine;
        private readonly IGameProgressService _gameProgressService;
        private readonly IGameConfigService _gameConfigService;

        public LoadProgressState(
            IStateMachineMover gameStateMachine,
            IGameProgressService gameProgressService,
            IGameConfigService gameConfigService)
        {
            _gameConfigService = gameConfigService;
            _gameStateMachine = gameStateMachine;
            _gameProgressService = gameProgressService;
        }

        public void Enter()
        {
            Debug.Log("Load Progress State");

            _gameProgressService.LoadProgress();
            _gameConfigService.Load();

            _gameStateMachine.Enter<LoadMenuState>();
        }

        public void Exit() { }
    }
}
