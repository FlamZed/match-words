using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Feature.LevelMenu.View
{
    public class LevelButtonView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelNameText;
        [SerializeField] private TMP_Text _levelProgressText;

        [Space]
        [SerializeField] private Image _medalImage;

        [Space, Header("States")]
        [SerializeField] private GameObject _unlockedState;
        [SerializeField] private GameObject _lockedState;

        [Space, Header("Clickable Button")]
        [SerializeField] private Button _clickableButton;

        private void Start() =>
            _clickableButton.onClick.AddListener(LoadLevel);

        private void OnDestroy() =>
            _clickableButton.onClick.RemoveListener(LoadLevel);

        public void Initialize()
        {

        }

        private void LoadLevel()
        {

        }
    }
}
