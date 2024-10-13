using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infrastructure.AssetManagement
{
    public class AssetFactory : IAssetFactory
    {
        public async UniTask<GameObject> Instantiate(string assetId, Transform underParent = null) =>
            await Instantiate<string>(assetId, underParent);

        public async UniTask<GameObject> Instantiate(AssetReference assetReference) =>
            await Instantiate<AssetReference>(assetReference);

        public async UniTask<GameObject> Instantiate(AssetLabelReference assetLabelReference) =>
            await Instantiate<AssetLabelReference>(assetLabelReference);

        private async UniTask<GameObject> Instantiate<TKey>(TKey asset, Transform underParent = null)
        {
            var asyncOperationHandle = Addressables.InstantiateAsync(asset, underParent);

            return await asyncOperationHandle.Task;
        }
    }
}
