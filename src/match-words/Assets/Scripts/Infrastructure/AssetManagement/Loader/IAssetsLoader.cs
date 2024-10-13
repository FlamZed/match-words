using System;
using Object = UnityEngine.Object;

namespace Infrastructure.AssetManagement
{
    public interface IAssetsLoader
    {
        T Load<T>(string path) where T : Object;
    }
}
