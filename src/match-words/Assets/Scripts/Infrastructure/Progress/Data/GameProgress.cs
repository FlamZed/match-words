using System;
using System.Collections.Generic;
using Infrastructure.Data.Type;
using Newtonsoft.Json;

namespace Infrastructure.Progress.Data
{
    [Serializable]
    public class GameProgress
    {
        [JsonProperty] public Dictionary<string, LevelProgress> WordsProgress = new();
        [JsonProperty] public int CurrentCoins = 0;
    }

    [Serializable]
    public class LevelProgress
    {
        [JsonProperty] public int LevelSeconds = 0;

        [JsonProperty] public List<string> PurchasedTipsWord = new();

        [JsonProperty] public List<string> CompletedWords = new();
        [JsonIgnore] public List<DictionaryEntry> TotalWords = new();

        public LevelProgress() { }
        public LevelProgress(string word) =>
            AddCompletedWord(word);

        public void AddCompletedWord(string word)
        {
            if (CompletedWords == null)
                CompletedWords = new List<string>();

            if (!CompletedWords.Contains(word))
                CompletedWords.Add(word);
        }

        public void SetTotalWords(List<DictionaryEntry> words) =>
            TotalWords = words;

        public float GetProgress() =>
            ((float)CompletedWords.Count / TotalWords.Count) * 100f;

        public bool IsWordUnlocked(DictionaryEntry word)
        {
            if (CompletedWords == null)
                return false;

            return CompletedWords.Contains(word.Word);
        }

        public void UnlockTip(string selectedWord)
        {
            PurchasedTipsWord.Add(selectedWord);
        }
    }
}
