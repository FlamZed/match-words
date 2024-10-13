using Cysharp.Threading.Tasks;
using Feature.LevelMenu.View;
using Infrastructure.GameManager;
using Infrastructure.GameManager.Behaviour;
using Infrastructure.Progress;
using UnityEngine;
using Zenject;

namespace Feature.LevelMenu
{
    public class LevelMenuController : EntryPointBehaviour<LevelMenuController>
    {
        [SerializeField] private LevelSelectView _levelSelectView;

        private IGameProgressService _gameProgressService;
        private IGameManager _gameManager;

        public UniTaskCompletionSource<string> LoadLevelCompletionSource;

        [Inject]
        private void Construct(IGameProgressService gameProgressService) =>
            _gameProgressService = gameProgressService;

        public void Initialize()
        {
            LoadLevelCompletionSource = new UniTaskCompletionSource<string>();

            var currentProgress = _gameProgressService.GetProgress();

            _levelSelectView.Initialize(currentProgress.WordsProgress);
            _levelSelectView.OnLevelSelected += LoadGameLevel;
        }

        private void LoadGameLevel(string levelName) =>
            LoadLevelCompletionSource.TrySetResult(levelName);
    }
}
