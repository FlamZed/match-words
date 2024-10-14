using System;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public interface ISceneLoader
    {
        public UniTask<SceneEntity> LoadSceneAsync(string sceneName,
            LoadSceneMode loadMode = LoadSceneMode.Single, bool activateOnLoad = true);

        public UniTask<SceneEntity> LoadSceneAsync(AssetReference assetReference,
            LoadSceneMode loadMode = LoadSceneMode.Single, bool activateOnLoad = true);

        public void LoadLocalScene(string name, Action onLoaded = null);
    }
}
