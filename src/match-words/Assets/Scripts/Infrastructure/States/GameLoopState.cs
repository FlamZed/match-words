using Infrastructure.Services.Audio;
using Infrastructure.Services.Audio.Type;
using Infrastructure.Services.Input;
using Infrastructure.Services.Level;
using UnityEngine;
using Zenject;

namespace Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _gameStateMachine;

        private readonly IInputService _inputService;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ISpawnPointService _spawnPoint;
        private readonly IGameResetService _gameResetService;
        private readonly IAudioService _audioService;

        private bool _isReleased = false;

        private Vector3 _spawnPointPosition;

        public GameLoopState(GameStateMachine gameStateMachine, DiContainer diContainer)
        {
            _gameStateMachine = gameStateMachine;

            _inputService = diContainer.Resolve<IInputService>();
            _coroutineRunner = diContainer.Resolve<ICoroutineRunner>();
            _spawnPoint = diContainer.Resolve<ISpawnPointService>();
            _gameResetService = diContainer.Resolve<IGameResetService>();
            _audioService = diContainer.Resolve<IAudioService>();
        }

        public void Enter()
        {
            _gameResetService.OnRestart += ToRestart;

            _spawnPointPosition = _spawnPoint.GetSpawnPoint;

            _audioService.PlayBackground(BackgroundClip.Game);
        }

        private void ToRestart() =>
            _gameStateMachine.Enter<LoadMenuState>();

        public void Exit()
        {
            _gameResetService.OnRestart -= ToRestart;
        }

        private void ToWinSate()
        {
            _audioService.PlayOneShot(AudioClipShot.Win);
            Debug.LogWarning("Win");
        }

        private void ToLoseState()
        {
            _audioService.PlayOneShot(AudioClipShot.Lose);
            Debug.LogWarning("Lose");
        }
    }
}
