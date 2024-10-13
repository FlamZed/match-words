using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Feature.LevelMenu.View
{
    public class LevelProgressView : MonoBehaviour
    {
        private const float SilverThreshold = 50f; //TODO: Should be in a configuration file
        private const float GoldThreshold = 80f;
        private const float CompletionThreshold = 100f;

        [Header("Level Progress States")]
        [SerializeField] private GameObject _progressState;
        [SerializeField] private GameObject _completedState;

        [Space]
        [SerializeField] private TMP_Text _progressText;

        [Space]
        [SerializeField] private Image _medalImage;

        [Space, Header("Medals Sprites")]
        [SerializeField] private Sprite _goldMedal;
        [SerializeField] private Sprite _silverMedal;
        [SerializeField] private Sprite _bronzeMedal;

        public void SetProgress(float progress)
        {
            if (progress >= CompletionThreshold)
            {
                SetCompleted();
            }
            else
            {
                SetInProgress();
                UpdateProgressText(progress);
                UpdateMedal(progress);
            }
        }

        private void SetCompleted() =>
            ToggleStates(false);

        private void SetInProgress() =>
            ToggleStates(true);

        private void ToggleStates(bool inProgress)
        {
            _progressState.SetActive(inProgress);
            _completedState.SetActive(!inProgress);
        }

        private void UpdateProgressText(float progress) =>
            _progressText.text = $"{(int)progress}%";

        private void UpdateMedal(float progress)
        {
            if (progress < SilverThreshold)
                _medalImage.sprite = _bronzeMedal;
            else if (progress < GoldThreshold)
                _medalImage.sprite = _silverMedal;
            else
                _medalImage.sprite = _goldMedal;
        }
    }
}
