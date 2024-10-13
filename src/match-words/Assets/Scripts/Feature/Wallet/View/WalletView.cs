using TMPro;
using UnityEngine;
using Zenject;

namespace Feature.Wallet.View
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coinsText;

        private IWalletService _walletService;

        [Inject]
        private void Construct(IWalletService walletService) =>
            _walletService = walletService;

        private void Awake() =>
            _coinsText.text = _walletService.CurrentCoins.ToString();

        private void Start() =>
            _walletService.OnCoinsUpdated += UpdateCoins;

        private void OnDestroy() =>
            _walletService.OnCoinsUpdated -= UpdateCoins;

        private void UpdateCoins() =>
            _coinsText.text = _walletService.CurrentCoins.ToString();
    }
}
