using Infrastructure.AssetManagement;
using Infrastructure.Services.Level;
using Logic.Loading;
using UnityEngine;
using Utility;
using Zenject;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private IGameStateMachine _stateMachine;

        private readonly ISceneLoader _sceneLoader;
        private readonly ILoadingCurtain _curtain;
        private readonly IAssets _assetProvider;
        private readonly ISpawnPointService _spawnPoint;

        public LoadLevelState(GameStateMachine stateMachine, DiContainer diContainer)
        {
            _stateMachine = stateMachine;

            _assetProvider = diContainer.Resolve<IAssets>();
            _sceneLoader = diContainer.Resolve<ISceneLoader>();
            _curtain = diContainer.Resolve<ILoadingCurtain>();
            _spawnPoint = diContainer.Resolve<ISpawnPointService>();
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();

            _sceneLoader.Load(sceneName, onLoaded: OnLoaded);
        }

        private void OnLoaded()
        {
            _stateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
            _curtain.Hide();
        }
    }
}
