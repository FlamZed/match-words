using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Feature.PrintMachine.Popups;
using Feature.PrintMachine.View.List.Factory;
using Infrastructure.Data.Type;
using Infrastructure.Progress.Data;
using Infrastructure.View.Factory;
using UnityEngine;
using Zenject;

namespace Feature.PrintMachine.View.List
{
    public class ListView : MonoBehaviour
    {
        [SerializeField] private Transform _content;

        private IListWordFactory _listWordFactory;
        private IViewFactory _viewFactory;

        private readonly List<ListWordView> _listWordViews = new();

        private LevelProgress _selectedLevel;
        private string _levelName;

        [Inject]
        private void Construct(
            IListWordFactory listWordFactory,
            IViewFactory viewFactory)
        {
            _viewFactory = viewFactory;
            _listWordFactory = listWordFactory;
        }

        private void OnDestroy()
        {
            foreach (var listWordView in _listWordViews)
                listWordView.OnWordSelected -= OpenHelpPopup;
        }

        public async void Initialize(string levelName, LevelProgress selectedLevel)
        {
            _levelName = levelName;
            _selectedLevel = selectedLevel;

            foreach (var word in selectedLevel.TotalWords)
            {
                var prefab = await _listWordFactory.Produce(_content);
                var listWordView = prefab.GetComponent<ListWordView>();

                listWordView.Initialize(word, selectedLevel.IsWordUnlocked(word));
                listWordView.OnWordSelected += OpenHelpPopup;

                _listWordViews.Add(listWordView);
            }
        }

        public void UnlockWord(DictionaryEntry word)
        {
            var listWordView = _listWordViews.Find(view => view.Word == word);
            listWordView.Unlock();
        }

        private async void OpenHelpPopup(DictionaryEntry word)
        {
            Debug.Log($"Selected word: {word.Word}");

            var helpView = (HelpView) await _viewFactory.Instantiate(new HelpViewType());

            helpView.Initialize(_levelName, word, _selectedLevel.IsWordUnlocked(word));
            await helpView.Show();
            helpView.Hide().Forget();
        }
    }
}
