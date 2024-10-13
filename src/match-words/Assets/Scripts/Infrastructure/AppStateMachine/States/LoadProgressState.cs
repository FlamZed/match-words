using Feature.Wallet;
using Infrastructure.AppStateMachine.Interfaces;
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
        private readonly IWalletService _walletService;

        public LoadProgressState(
            IStateMachineMover gameStateMachine,
            IGameProgressService gameProgressService,
            IGameConfigService gameConfigService,
            IWalletService walletService)
        {
            _walletService = walletService;
            _gameConfigService = gameConfigService;
            _gameStateMachine = gameStateMachine;
            _gameProgressService = gameProgressService;
        }

        public void Enter()
        {
            Debug.Log("Load Progress State");

            _gameConfigService.Load();
            _gameProgressService.LoadProgress();

            _walletService.Initialize(_gameProgressService.GetCoins());

            _gameProgressService.RestoreWordsData(_gameConfigService.WordDictionary);

            _gameStateMachine.Enter<LoadMenuState>();
        }

        public void Exit() { }
    }
}
