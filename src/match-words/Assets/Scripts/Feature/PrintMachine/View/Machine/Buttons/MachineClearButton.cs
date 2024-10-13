using System;

namespace Feature.PrintMachine.View
{
    public class MachineClearButton : MachineButtonBase
    {
        public event Action OnClicked = delegate { };

        private void Start() =>
            button.onClick.AddListener(SelectButton);

        private void OnDestroy() =>
            button.onClick.RemoveListener(SelectButton);

        private void SelectButton() =>
            OnClicked?.Invoke();
    }
}
