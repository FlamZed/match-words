using Infrastructure.Data.Type;
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

        public GameProgress GetProgress() =>
            _currentProgress;

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

        public void RestoreWordsData(GroupedWords wordDictionary)
        {
            foreach (var wordGroup in wordDictionary.Words)
            {
                if (_currentProgress.WordsProgress.TryGetValue(wordGroup.Key, out var completedWords))
                {
                    completedWords.SetTotalWords(wordGroup.Value);
                }
                else
                {
                    var levelProgress = new LevelProgress();
                    levelProgress.SetTotalWords(wordGroup.Value);
                    _currentProgress.WordsProgress.Add(wordGroup.Key, levelProgress);
                }
            }
        }

        private void CreateNewProgress()
        {
            _currentProgress = new GameProgress();
            SaveProgress();
        }

        public void UpdateProgress(string key, string value)
        {
            if (_currentProgress == null)
                CreateNewProgress();

            if (_currentProgress.WordsProgress.TryGetValue(key, out var completedWords))
                completedWords.AddCompletedWord(value);
            else
                _currentProgress.WordsProgress.Add(key, new(value));

            SaveProgress();
        }

        public void SaveCoins(int currentCoins)
        {
            if (_currentProgress == null)
                CreateNewProgress();

            _currentProgress.CurrentCoins = currentCoins;
            SaveProgress();
        }

        public int GetCoins()
        {
            if (_currentProgress == null)
                CreateNewProgress();

            return _currentProgress.CurrentCoins;
        }

        public void UpdateLevelTime(string levelName, int time)
        {
            if (_currentProgress == null)
                CreateNewProgress();

            if (_currentProgress.WordsProgress.TryGetValue(levelName, out var levelProgress))
                levelProgress.LevelSeconds = time;
            else
                _currentProgress.WordsProgress.Add(levelName, new() { LevelSeconds = time });

            SaveProgress();
        }

        public void UnlockTipFor(string levelName, string selectedWord)
        {
            if (_currentProgress == null)
                CreateNewProgress();

            if (_currentProgress.WordsProgress.TryGetValue(levelName, out var levelProgress))
                levelProgress.UnlockTip(selectedWord);

            SaveProgress();
        }

        public bool IsTipPurchasedFor(string levelName, string selectedWord)
        {
            if (_currentProgress == null)
                CreateNewProgress();

            if (_currentProgress.WordsProgress.TryGetValue(levelName, out var levelProgress))
                return levelProgress.PurchasedTipsWord.Contains(selectedWord);

            return false;
        }
    }
}
