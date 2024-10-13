using Infrastructure.Data.Type;
using Infrastructure.Progress.Data;

namespace Infrastructure.Progress
{
    public interface IGameProgressService
    {
        void SaveProgress();
        void LoadProgress();
        void RestoreWordsData(GroupedWords wordDictionary);
        GameProgress GetProgress();
        void UpdateProgress(string key, string value);
        void SaveCoins(int currentCoins);
        int GetCoins();
        void UpdateLevelTime(string levelName, int time);
        void UnlockTipFor(string levelName, string selectedWord);
        bool IsTipPurchasedFor(string levelName, string selectedWord);
    }
}
