using UnityEngine;

namespace Infrastructure.AssetManagement.Loader
{
    public class AssetLoaderProvider : IAssetsLoader
    {
        public T Load<T>(string path) where T : Object
            => Resources.Load<T>(path);
    }
}
