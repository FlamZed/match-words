using Feature.LoadingCurtain;
using Infrastructure.AppStateMachine;
using Infrastructure.AppStateMachine.States;
using Infrastructure.AssetManagement;
using Infrastructure.Data;
using Infrastructure.DIContainer.Extensions;
using Infrastructure.Progress;
using Infrastructure.Serialization;
using Infrastructure.Services.Audio;
using Infrastructure.View.Factory;
using UnityEngine;
using Zenject;

namespace Infrastructure.DIContainer.ProjectInstallers
{
    public class BootstrapInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private GameObject _curtainPrefab;
        [SerializeField] private GameObject _coroutineRunner;
        [SerializeField] private GameObject _audioService;

        [Space]
        [SerializeField] private Canvas _popupCanvas;

        public override void InstallBindings()
        {
            BindCurtain();
            BindServices();
            BindView();
            BindStateMachine();
        }

        private void BindCurtain()
        {
            Container.BindService<ILoadingCurtain, LoadingCurtain>(_curtainPrefab);
        }

        private void BindServices()
        {
            Container.BindService<ICoroutineRunner, CoroutineRunner>(_coroutineRunner);

            Container.BindService<IAudioService, AudioService>(_audioService);

            Container.BindService<ISerializeService, JsonSerializeService>();
            Container.BindService<IGameProgressService, GameProgressService>();

            Container.BindService<ISceneLoader, SceneLoader>();
            Container.BindService<IAssetsLoader, AssetLoaderProvider>();

            Container.BindService<IGameConfigService, GameConfigService>();
        }

        private void BindView()
        {
            Container.BindService<IAssetFactory, AssetFactory>();

            Container
                .Bind<IViewFactory>()
                .To<ViewFactory>()
                .AsSingle()
                .WithArguments(_popupCanvas)
                .NonLazy();
        }

        private void BindStateMachine()
        {
            Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle().NonLazy();

            Container.BindStateMachine<GameStateMachine>()
                .BindState<BootstrapState>()
                .BindState<LoadProgressState>()
                .BindState<LoadMenuState>()
                .BindState<MainMenuState>()
                .BindState<LoadLevelState>()
                .BindState<GameLoopState>();
        }

        public void Initialize()
        {
            Debug.Log("Bootstrap Installer Initialized");

            Container.Resolve<IGameStateMachine>().Enter<BootstrapState>();
        }
    }
}
