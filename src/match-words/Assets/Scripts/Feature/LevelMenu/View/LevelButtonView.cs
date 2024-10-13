using System;
using Infrastructure.Progress.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Feature.LevelMenu.View
{
    public class LevelButtonView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelNameText;

        [Space]
        [SerializeField] private LevelProgressView _progressView;

        [Space, Header("States")]
        [SerializeField] private GameObject _unlockedState;
        [SerializeField] private GameObject _lockedState;

        [Space, Header("Clickable Button")]
        [SerializeField] private Button _clickableButton;

        public event Action<string> OnLevelSelected = delegate { };

        private string _levelIdentifier;

        private void Start() =>
            _clickableButton.onClick.AddListener(LoadLevel);

        private void OnDestroy() =>
            _clickableButton.onClick.RemoveListener(LoadLevel);

        public void Initialize(string levelName, LevelProgress levelProgress)
        {
            _levelIdentifier = levelName;

            SetActiveState(true);

            _levelNameText.text = levelName;

            _progressView.SetProgress(levelProgress.GetProgress());
        }

        public void SetInactive()
        {
            SetActiveState(false);
        }

        private void SetActiveState(bool isActive)
        {
            _unlockedState.SetActive(isActive);
            _lockedState.SetActive(!isActive);
        }

        private void LoadLevel() =>
            OnLevelSelected?.Invoke(_levelIdentifier);
    }
}
