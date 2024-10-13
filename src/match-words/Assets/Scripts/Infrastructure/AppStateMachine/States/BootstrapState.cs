using Infrastructure.AppStateMachine.Interfaces;
using UnityEngine.SceneManagement;

namespace Infrastructure.AppStateMachine.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Boot";

        private readonly IStateMachineMover _gameStateMachine;

        private readonly ISceneLoader _sceneLoader;

        public BootstrapState(
            IStateMachineMover gameStateMachine,
            ISceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            if (SceneManager.GetActiveScene().name != Initial)
                _sceneLoader.Load(Initial, onLoaded: LoadEnterState);
            else
                LoadEnterState();
        }

        private void LoadEnterState()
        {
            _gameStateMachine.Enter<LoadProgressState>();
        }

        public void Exit()
        {
        }
    }
}
