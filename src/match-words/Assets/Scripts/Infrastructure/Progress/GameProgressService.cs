using Infrastructure.Progress.Data;
using Infrastructure.Serialization;
using UnityEngine;

namespace Infrastructure.Progress
{
    public class GameProgressService : IGameProgressService
    {
        private const string ProgressKey = "progress";

        private readonly ISerializeService _serializeService;

        private GameProgress _currentProgress;

        public GameProgressService(ISerializeService serializeService) =>
            _serializeService = serializeService;

        public void SaveProgress()
        {
            if (_serializeService.Serialize(_currentProgress, out var progressJson))
                PlayerPrefs.SetString(ProgressKey, progressJson);
        }

        public void LoadProgress()
        {
            var progressJson = PlayerPrefs.GetString(ProgressKey, "");

            _currentProgress = _serializeService.Deserialize(progressJson, out GameProgress progress)
                ? progress :
                new GameProgress();
        }

        private void CreateNewProgress()
        {
            _currentProgress = new GameProgress();
            SaveProgress();
        }

        public void UpdateProgress(string key)
        {
            _currentProgress.WordsProgress[key]++;
            SaveProgress();
        }
    }

    public interface IGameProgressService
    {
        void SaveProgress();
        void LoadProgress();
    }
}
