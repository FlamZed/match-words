using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Progress.Data;
using UnityEngine;

namespace Feature.LevelMenu.View
{
    public class LevelSelectView : MonoBehaviour
    {
        [SerializeField] private List<LevelButtonView> _levelButtons;

        public event Action<string> OnLevelSelected = delegate { };

        private void Start()
        {
            foreach (var levelButton in _levelButtons)
                levelButton.OnLevelSelected += SelectLevel;
        }

        private void OnDestroy()
        {
            foreach (var levelButton in _levelButtons)
                levelButton.OnLevelSelected -= SelectLevel;
        }

        public void Initialize(Dictionary<string, LevelProgress> levels)
        {
            List<string> levelsNames = levels.Keys.ToList();

            for (int index = 0; index < _levelButtons.Count; index++)
            {
                var levelButton = _levelButtons[index];

                if (index < levelsNames.Count)
                {
                    var levelName = levelsNames[index];
                    var levelProgress = levels[levelName];

                    levelButton.Initialize(levelName, levelProgress);
                }
                else
                {
                    levelButton.SetInactive();
                }
            }
        }

        private void SelectLevel(string levelName) =>
            OnLevelSelected?.Invoke(levelName);
    }
}
