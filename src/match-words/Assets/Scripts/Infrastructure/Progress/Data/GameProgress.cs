using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Infrastructure.Progress.Data
{
    [Serializable]
    public class GameProgress
    {
        [JsonProperty] public Dictionary<string, int> WordsProgress = new();
    }
}
