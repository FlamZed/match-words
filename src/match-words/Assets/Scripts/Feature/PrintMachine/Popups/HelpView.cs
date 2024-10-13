using Cysharp.Threading.Tasks;
using Feature.Wallet;
using Infrastructure.Data.Type;
using Infrastructure.Progress;
using Infrastructure.View;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Feature.PrintMachine.Popups
{
    public class HelpView : MonoBehaviour, IView
    {
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _descriptionText;

        [Space, Header("Blur Effect")]
        [SerializeField] private GameObject _blurEffect;

        [Space, Header("Buttons")]
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _claimButton;

        private IWalletService _walletService;
        private IGameProgressService _gameProgressService;

        private UniTaskCompletionSource _closeButtonClicked;

        private string _levelName;
        private string _selectedWord;

        [Inject]
        private void Construct(
            IWalletService walletService,
            IGameProgressService gameProgressService)
        {
            _gameProgressService = gameProgressService;
            _walletService = walletService;
        }

        public void Initialize(string levelName, DictionaryEntry word, bool isUnlocked)
        {
            _levelName = levelName;
            _selectedWord = word.Word;

            _titleText.text = isUnlocked ? word.Word : word.HiddenWord;
            _descriptionText.text = word.Description;

            SetBlurEffect(isUnlocked || _gameProgressService.IsTipPurchasedFor(levelName, _selectedWord));
        }

        private void SetBlurEffect(bool isUnlocked)
        {
            _claimButton.gameObject.SetActive(!isUnlocked);
            _blurEffect.SetActive(!isUnlocked);
        }

        public UniTask Show()
        {
            _closeButtonClicked = new UniTaskCompletionSource();

            RegisterButtons();

            return _closeButtonClicked.Task;
        }

        public UniTask Hide()
        {
            UnregisterButtons();

            Destroy(gameObject);

            return UniTask.CompletedTask;
        }

        private void RegisterButtons()
        {
            _closeButton.onClick.AddListener(OnCloseButtonClicked);
            _claimButton.onClick.AddListener(OnBuyButtonClicked);
        }

        private void UnregisterButtons()
        {
            _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
            _claimButton.onClick.RemoveListener(OnBuyButtonClicked);
        }

        private void OnCloseButtonClicked() =>
            _closeButtonClicked.TrySetResult();

        private void OnBuyButtonClicked()
        {
            if (_walletService.CanBePurchased(10))
            {
                _walletService.RemoveCoins(10);
                _gameProgressService.UnlockTipFor(_levelName, _selectedWord);
                UnlockTip();
            }
        }

        private void UnlockTip()
        {
            _claimButton.gameObject.SetActive(false);
            _blurEffect.SetActive(false);
        }
    }
}
