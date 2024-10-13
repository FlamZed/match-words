using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infrastructure.AssetManagement
{
    public interface IAssetFactory
    {
        public UniTask<GameObject> Instantiate(string assetId, Transform underParent = null);

        public UniTask<GameObject> Instantiate(AssetReference assetReference);

        public UniTask<GameObject> Instantiate(AssetLabelReference assetLabelReference);
    }
}
