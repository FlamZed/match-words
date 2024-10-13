using System.Collections.Generic;

namespace Infrastructure.Data.Type
{
    public class GroupedWords
    {
        public Dictionary<string, List<DictionaryEntry>> Words { get; set; }

        public GroupedWords(List<string> keys)
        {
            Words = new Dictionary<string, List<DictionaryEntry>>();

            foreach (var key in keys)
                Words[key] = new List<DictionaryEntry>();
        }
    }
}
