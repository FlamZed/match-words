using System;
using System.Collections.Generic;
using System.Linq;
using Feature.PrintMachine.View;
using Feature.PrintMachine.View.List;
using Infrastructure.Data.Type;
using Infrastructure.GameManager.Behaviour;
using Infrastructure.Progress.Data;
using TMPro;
using UnityEngine;

namespace Feature.PrintMachine
{
    public class PrintMachineController : EntryPointBehaviour<PrintMachineController>
    {
        [SerializeField] private TMP_Text _levelNameText;

        [Space, Header("Print Machine View")]
        [SerializeField] private PrintMachineView _printMachineView;

        [Space, Header("List Word View")]
        [SerializeField] private ListView _listView;

        [Space, Header("Words Counter")]
        [SerializeField] private TMP_Text _wordsCounterText;

        [Space, Header("Timer")]
        [SerializeField] private TMP_Text _totalTimeText;

        private List<DictionaryEntry> _wordsList = new();

        public event Action<DictionaryEntry> OnWordCompleted = delegate { };
        public event Action OnLeaveButtonPressed = delegate { };

        public void Initialize(string levelName, LevelProgress selectedLevel)
        {
            _wordsList = selectedLevel.TotalWords;

            _levelNameText.text = levelName;

            _printMachineView.Initialize(levelName);
            _listView.Initialize(levelName, selectedLevel);
            
            _printMachineView.OnPrintWordUpdated += TryToUnlockWord;
        }

        public void UpdateWordCounter(int totalWords, int unlockedWords) =>
            _wordsCounterText.text = $"{unlockedWords}/{totalWords}";

        public void UpdateTimer(int time)
        {
            var minutes = time / 60;
            var seconds = time % 60;

            _totalTimeText.text = $"{minutes:00}:{seconds:00}";
        }

        private void TryToUnlockWord(string printedWord)
        {
            if (_wordsList.Exists(word => word.Word == printedWord))
            {
                var wordView = _wordsList.FirstOrDefault(word => word.Word == printedWord);

                if (wordView == null)
                    return;

                _listView.UnlockWord(wordView);

                _printMachineView.RestoreButtons();

                OnWordCompleted?.Invoke(wordView);
            }
        }
    }
}
