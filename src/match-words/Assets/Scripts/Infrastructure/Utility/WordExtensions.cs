using System.Text;

namespace Infrastructure.Utility
{
    public static class WordExtensions
    {
        public static string HideWord(this string word)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < word.Length; i++)
                stringBuilder.Append("_");

            return stringBuilder.ToString();
        }
    }
}
