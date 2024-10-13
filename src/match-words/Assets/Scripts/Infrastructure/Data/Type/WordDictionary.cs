using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Infrastructure.Data.Type
{
    [Serializable]
    public class WordDictionary
    {
        [JsonProperty] public List<string> LevelWords { get; set; }
        [JsonProperty] public List<DictionaryEntry> Dictionary { get; set; }

        public WordDictionary()
        {
            LevelWords = new List<string>();
            Dictionary = new List<DictionaryEntry>();
        }
    }

    [Serializable]
    public class DictionaryEntry
    {
        [JsonProperty] public string Word { get; set; }
        [JsonProperty] public string Description { get; set; }
        [JsonProperty] public string Category { get; set; }
    }
}
