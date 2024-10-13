using System;
using Infrastructure.Data.Type;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Feature.PrintMachine.View.List
{
    public class ListWordView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [Space]
        [SerializeField] private TMP_Text _titleText;

        private DictionaryEntry _word;

        public event Action<DictionaryEntry> OnWordSelected = delegate { };

        public DictionaryEntry Word => _word;

        private void Start() =>
            _button.onClick.AddListener(Select);

        private void OnDestroy() =>
            _button.onClick.RemoveListener(Select);

        public void Initialize(DictionaryEntry word, bool isUnlocked)
        {
            _word = word;

            _titleText.text = isUnlocked
                ? word.Word
                : word.HiddenWord;
        }

        private void Select() =>
            OnWordSelected?.Invoke(_word);

        public void Unlock() =>
            _titleText.text = _word.Word;
    }
}
