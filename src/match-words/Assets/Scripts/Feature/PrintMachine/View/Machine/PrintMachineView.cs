using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Services.Audio;
using Infrastructure.Services.Audio.Type;
using TMPro;
using UnityEngine;
using Zenject;

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

        private IAudioService _audioService;

        public event Action<string> OnPrintWordUpdated = delegate { };

        private readonly StringBuilder _printedWord = new();

        private readonly List<MachinePrintButton> _usedMachineButtons = new();

        [Inject]
        private void Construct(IAudioService audioService) =>
            _audioService = audioService;

        private void Start()
        {
            clearPrintButton.OnClicked += RestoreButtons;
            removeCharPrintButton.OnClicked += OnRemoveLetterPrintButtonClicked;
        }

        private void OnDestroy()
        {
            clearPrintButton.OnClicked -= RestoreButtons;
            removeCharPrintButton.OnClicked -= OnRemoveLetterPrintButtonClicked;

            for (int i = 0; i < _machineButtons.Count; i++)
                _machineButtons[i].OnLetterSelected -= PrintWord;
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
            _audioService.PlayOneShot(AudioClipShot.Kick);

            _usedMachineButtons.Add(button);

            _printedWord.Append(button.Letter);
            _printedWordText.text = _printedWord.ToString();

            OnPrintWordUpdated?.Invoke(_printedWord.ToString());
        }

        private void OnRemoveLetterPrintButtonClicked()
        {
            _audioService.PlayOneShot(AudioClipShot.Kick);

            if (_printedWord.Length == 0)
                return;

            _printedWord.Remove(_printedWord.Length - 1, 1);
            _printedWordText.text = _printedWord.ToString();

            _usedMachineButtons[^1].Unselect();
            _usedMachineButtons.RemoveAt(_usedMachineButtons.Count - 1);
        }

        public void RestoreButtons()
        {
            _audioService.PlayOneShot(AudioClipShot.Kick);

            _printedWord.Clear();
            _printedWordText.text = string.Empty;

            _usedMachineButtons.ForEach(x => x.Unselect());
            _usedMachineButtons.Clear();
        }
    }
}
