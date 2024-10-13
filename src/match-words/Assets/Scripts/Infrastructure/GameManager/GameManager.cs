using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Infrastructure.GameManager
{
    public class GameManager : IGameManager
    {
        private readonly Dictionary<System.Type, object> _serviceLoaders = new();

        public UniTask<TService> AwaitServiceLoading<TService>() where TService : MonoBehaviour
        {
            var serviceType = typeof(TService);

            if (_serviceLoaders.TryGetValue(serviceType, out var existingServiceTask))
                return (UniTask<TService>)existingServiceTask;

            var taskCompletionSource = new UniTaskCompletionSource<TService>();
            _serviceLoaders[serviceType] = taskCompletionSource.Task;

            return taskCompletionSource.Task;
        }

        public void SetServiceLoaded<TService>(TService serviceInstance) where TService : MonoBehaviour
        {
            var serviceType = typeof(TService);

            if (_serviceLoaders.TryGetValue(serviceType, out var existingServiceTask))
            {
                var taskCompletionSource = existingServiceTask as UniTaskCompletionSource<TService>;
                taskCompletionSource?.TrySetResult(serviceInstance);
            }
            else
            {
                var taskCompletionSource = new UniTaskCompletionSource<TService>();
                taskCompletionSource.TrySetResult(serviceInstance);
                _serviceLoaders[serviceType] = taskCompletionSource.Task;
            }
        }

        public void DisposeService<TService>(TService serviceInstance) where TService : MonoBehaviour
        {
            var serviceType = typeof(TService);

            if (_serviceLoaders.ContainsKey(serviceType))
                _serviceLoaders.Remove(serviceType);
        }
    }
}
