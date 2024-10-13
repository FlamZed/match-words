using Cysharp.Threading.Tasks;
using Infrastructure.View;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

        private UniTaskCompletionSource _closeButtonClicked;

        public void Initialize()
        {

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

            return UniTask.CompletedTask;
        }

        private void RegisterButtons()
        {
            _closeButton.onClick.AddListener(OnCloseButtonClicked);
            _claimButton.onClick.AddListener(OnCloseButtonClicked);
        }

        private void UnregisterButtons()
        {
            _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
            _claimButton.onClick.RemoveListener(OnCloseButtonClicked);
        }

        private void OnCloseButtonClicked() =>
            _closeButtonClicked.TrySetResult();
    }
}
