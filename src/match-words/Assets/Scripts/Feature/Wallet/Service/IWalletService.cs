using System;

namespace Feature.Wallet
{
    public interface IWalletService
    {
        int CurrentCoins { get; }
        void Initialize(int coins);
        void AddCoins(int coins);
        void RemoveCoins(int coins);
        event Action OnCoinsUpdated;
        bool CanBePurchased(int i);
    }
}
