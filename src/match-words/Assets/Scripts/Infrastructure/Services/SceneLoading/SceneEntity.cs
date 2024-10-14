using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.Exceptions;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Infrastructure
{
    public class SceneEntity : IEntity
    {
        private AsyncOperationHandle<SceneInstance> handle;

        public SceneInstance Value { get; private set; }

        public SceneEntity(AsyncOperationHandle<SceneInstance> handle)
        {
            this.handle = handle;

            if (handle.IsDone)
                SetValue();
            else
                handle.Completed += OnHandleCompleted;
        }

        public void Dispose()
        {
            if (Value.Scene == default)
                return;

            if (Value.Scene.isLoaded)
                Addressables.UnloadSceneAsync(Value);

            if (handle.IsValid())
                Addressables.Release(handle);
        }

        private void OnHandleCompleted(AsyncOperationHandle<SceneInstance> handle)
        {
            handle.Completed -= OnHandleCompleted;

            SetValue();
        }

        private void SetValue()
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
                Value = handle.Task.Result;
            else
                throw new OperationException(handle.OperationException.Message, handle.OperationException.InnerException);
        }
    }
}
