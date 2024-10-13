using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Infrastructure.GameManager
{
    public interface IGameManager
    {
        UniTask<TService> AwaitServiceLoading<TService>() where TService : MonoBehaviour;
        void SetServiceLoaded<TService>(TService serviceInstance) where TService : MonoBehaviour;
        void DisposeService<TService>(TService serviceInstance) where TService : MonoBehaviour;
    }
}
