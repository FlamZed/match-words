using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class SceneProviderService : ISceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneProviderService(ICoroutineRunner coroutineRunner) =>
            _coroutineRunner = coroutineRunner;

        public async UniTask<SceneEntity> LoadSceneAsync(string sceneName,
            LoadSceneMode loadMode = LoadSceneMode.Single, bool activateOnLoad = true) =>
            await LoadScene(sceneName, loadMode, activateOnLoad);

        public async UniTask<SceneEntity> LoadSceneAsync(AssetReference assetReference,
            LoadSceneMode loadMode = LoadSceneMode.Single, bool activateOnLoad = true) =>
            await LoadScene(assetReference, loadMode, activateOnLoad);

        public void LoadLocalScene(string name, Action onLoaded = null) =>
            _coroutineRunner.Run(LoadScene(name, onLoaded: onLoaded));

        private async UniTask<SceneEntity> LoadScene<TKey>(TKey scene, LoadSceneMode loadMode = LoadSceneMode.Single,
            bool activateOnLoad = true)
        {
            var asyncOperationHandle = Addressables.LoadSceneAsync(scene, loadMode, activateOnLoad);
            return await GetSceneEntity(asyncOperationHandle);
        }

        private async UniTask<SceneEntity> GetSceneEntity(AsyncOperationHandle<SceneInstance> handle)
        {
            await handle.Task;

            return new SceneEntity(handle);
        }

        private IEnumerator LoadScene(string name, Action onLoaded)
        {
            Debug.Log("Loading scene: " + name + "...");

            if (SceneManager.GetActiveScene().name == name)
            {
                onLoaded?.Invoke();

                yield break;
            }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(name);

            while (!waitNextScene.isDone)
                yield return null;

            onLoaded?.Invoke();
        }
    }
}
