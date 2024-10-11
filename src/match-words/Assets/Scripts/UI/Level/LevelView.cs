using Infrastructure.Services.Level;
using UnityEngine;
using Zenject;

namespace UI.Level
{
    public class LevelView : MonoBehaviour
    {
        [Header("Spawn Point")]
        [SerializeField] private Transform _spawnPoint;

        [Header("Popup")]
        [SerializeField] private GameObject _congratulationPopup;
        [SerializeField] private GameObject _losingPopup;

        [Inject] private ISpawnPointService _spawnPointService;
        [Inject] private IGameResetService _gameResetService;

        private void Start()
        {
            _spawnPointService.AllowSpawn(_spawnPoint.position);
        }

        public void Restart() =>
            _gameResetService.RestartLevel();

        private void ShowCongratulationPopup() =>
            _congratulationPopup.SetActive(true);

        private void ShowLosingPopup() =>
            _losingPopup.SetActive(true);
    }
}
