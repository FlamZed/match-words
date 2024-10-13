using System;
using Infrastructure.Progress;

namespace Feature.Wallet
{
    public class WalletService : IWalletService
    {
        private readonly IGameProgressService _gameProgressService;

        public event Action OnCoinsUpdated = delegate { };

        public int CurrentCoins { get; private set; }

        public WalletService(IGameProgressService gameProgressService) =>
            _gameProgressService = gameProgressService;

        public void Initialize(int coins) =>
            CurrentCoins = coins;

        public void AddCoins(int coins)
        {
            CurrentCoins += coins;

            _gameProgressService.SaveCoins(CurrentCoins);

            OnCoinsUpdated?.Invoke();
        }

        public void RemoveCoins(int coins)
        {
            CurrentCoins -= coins;

            _gameProgressService.SaveCoins(CurrentCoins);

            OnCoinsUpdated?.Invoke();
        }
        
        public bool CanBePurchased(int count) =>
            CurrentCoins >= count;
    }
}
