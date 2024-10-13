using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagement;
using UnityEngine;

namespace Feature.PrintMachine.View.List.Factory
{
    public class ListWordFactory : IListWordFactory
    {
        private const string ListWordAssetName = "WordView";

        private readonly IAssetFactory _assetFactory;

        public ListWordFactory(IAssetFactory assetFactory) =>
            _assetFactory = assetFactory;

        public UniTask<GameObject> Produce(Transform parent) =>
            _assetFactory.Instantiate(ListWordAssetName, parent);
    }
}
