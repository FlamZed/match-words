using System;
using TMPro;
using UnityEngine;

namespace Feature.PrintMachine.View
{
    public class MachinePrintButton : MachineButtonBase
    {
        [Space]
        [SerializeField] private TMP_Text _letterText;

        private char _letter;

        public event Action<MachinePrintButton> OnLetterSelected = delegate { };

        public char Letter => _letter;

        private void Start() =>
            button.onClick.AddListener(Select);

        private void OnDestroy() =>
            button.onClick.RemoveListener(Select);

        public void Initialize(char letter)
        {
            button.interactable = true;
            _letterText.gameObject.SetActive(true);

            _letter = letter;
            _letterText.text = letter.ToString();
        }

        public void SetInactive()
        {
            button.interactable = false;
            _letterText.gameObject.SetActive(false);
        }

        public void Select()
        {
            button.interactable = false;
            image.sprite = selectedSprite;

            _letterText.gameObject.SetActive(false);

            OnLetterSelected?.Invoke(this);
        }

        public void Unselect()
        {
            button.interactable = true;

            image.sprite = unselectedSprite;
            _letterText.gameObject.SetActive(true);
        }
    }
}
