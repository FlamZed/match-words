using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagement;
using UnityEngine;
using Zenject;

namespace Infrastructure.View.Factory
{
    public class ViewFactory : IViewFactory
    {
        private readonly DiContainer _container;
        private readonly IAssetFactory _assetFactory;
        private readonly Canvas _canvas;

        public ViewFactory(
            DiContainer container,
            IAssetFactory assetFactory,
            Canvas canvas)
        {
            _canvas = canvas;
            _assetFactory = assetFactory;
            _container = container;
        }

        public async UniTask<IView> Instantiate(ViewType viewType)
        {
            var asset = await _assetFactory.Instantiate(viewType.Value, _canvas.transform);
            _container.InjectGameObject(asset);

            return asset.GetComponent<IView>();
        }
    }
}
