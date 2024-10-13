using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace Feature.PrintMachine.View
{
    public class PrintMachineView : MonoBehaviour
    {
        [SerializeField] private List<MachinePrintButton> _machineButtons;

        [Space, Header("Machine Buttons")]
        [SerializeField] private MachineClearButton clearPrintButton;
        [SerializeField] private MachineClearButton removeCharPrintButton;

        [Space]
        [SerializeField] private TMP_Text _printedWordText;

        public event Action<string> OnPrintWordUpdated = delegate { };

        private readonly StringBuilder _printedWord = new();

        private readonly List<MachinePrintButton> _usedMachineButtons = new();

        private void Start()
        {
            clearPrintButton.OnClicked += RestoreButtons;
            removeCharPrintButton.OnClicked += OnRemoveLetterPrintButtonClicked;
        }

        private void OnDestroy()
        {
            clearPrintButton.OnClicked -= RestoreButtons;
            removeCharPrintButton.OnClicked -= OnRemoveLetterPrintButtonClicked;
        }

        public void Initialize(string word)
        {
            _printedWordText.text = string.Empty;

            var letters = word.ToCharArray();

            _machineButtons.ForEach(button => button.SetInactive());

            for (int i = 0; i < letters.Length; i++)
            {
                _machineButtons[i].Initialize(letters[i]);
                _machineButtons[i].OnLetterSelected += PrintWord;
            }
        }

        private void PrintWord(MachinePrintButton button)
        {
            _usedMachineButtons.Add(button);

            _printedWord.Append(button.Letter);
            _printedWordText.text = _printedWord.ToString();

            OnPrintWordUpdated?.Invoke(_printedWord.ToString());
        }

        private void OnRemoveLetterPrintButtonClicked()
        {
            if (_printedWord.Length == 0)
                return;

            _printedWord.Remove(_printedWord.Length - 1, 1);
            _printedWordText.text = _printedWord.ToString();

            _usedMachineButtons[^1].Unselect();
            _usedMachineButtons.RemoveAt(_usedMachineButtons.Count - 1);
        }

        public void RestoreButtons()
        {
            _printedWord.Clear();
            _printedWordText.text = string.Empty;

            _usedMachineButtons.ForEach(x => x.Unselect());
            _usedMachineButtons.Clear();
        }
    }
}
