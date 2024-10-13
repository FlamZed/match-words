using UnityEngine;
using Zenject;

namespace Infrastructure.GameManager.Behaviour
{
    public abstract class EntryPointBehaviour<T> : MonoBehaviour where T : EntryPointBehaviour<T>
    {
        private IGameManager _gameManager;

        [Inject]
        private void Construct(IGameManager gameManager) =>
            _gameManager = gameManager;

        protected virtual void Start() =>
            _gameManager.SetServiceLoaded(this as T);

        protected virtual void OnDestroy() =>
            _gameManager.DisposeService(this as T);
    }
}
