using System.Collections.Generic;
using Infrastructure.AssetManagement;
using Infrastructure.Data.Type;
using Infrastructure.Serialization;
using UnityEngine;

namespace Infrastructure.Data
{
    public class GameConfigService : IGameConfigService
    {
        private readonly IAssetsLoader _assetsLoader;
        private readonly ISerializeService _serializeService;

        private WordDictionary _wordDictionary;

        private GroupedWords _groupedWords;

        public GroupedWords WordDictionary => _groupedWords;

        public GameConfigService(
            IAssetsLoader assetsLoader,
            ISerializeService serializeService)
        {
            _serializeService = serializeService;
            _assetsLoader = assetsLoader;
        }

        public void Load()
        {
            var textAsset = _assetsLoader.Load<TextAsset>(AssetPath.GameConfig);

            if (_serializeService.Deserialize<WordDictionary>(textAsset.text, out var wordDictionary))
                _wordDictionary = wordDictionary;
            else
                _wordDictionary = new WordDictionary();

            GroupedWords();
        }

        private void GroupedWords()
        {
            _groupedWords = new GroupedWords(_wordDictionary.LevelWords);

            foreach (var levelWord in _wordDictionary.LevelWords)
            {
                var matchingWords = new List<DictionaryEntry>();

                foreach (var entry in _wordDictionary.Dictionary)
                {
                    if (ContainsAllLetters(levelWord, entry.Word))
                    {
                        matchingWords.Add(entry);
                    }
                }

                if (matchingWords.Count > 0)
                {
                    _groupedWords.Words[levelWord] = matchingWords;
                }
            }
        }

        private bool ContainsAllLetters(string levelWord, string word) // TODO: Refactor, Not optimized for large dictionaries
        {
            List<char> levelWordLetters = new List<char>(levelWord.ToLower());

            foreach (var letter in word)
            {
                if (levelWordLetters.Contains(letter))
                    levelWordLetters.Remove(letter);
                else
                    return false;
            }

            return true;
        }
    }

    public interface IGameConfigService
    {
        GroupedWords WordDictionary { get; }
        void Load();
    }
}
