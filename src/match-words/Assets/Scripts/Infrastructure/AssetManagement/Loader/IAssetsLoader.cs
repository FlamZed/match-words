using Object = UnityEngine.Object;

namespace Infrastructure.AssetManagement.Loader
{
    public interface IAssetsLoader
    {
        T Load<T>(string path) where T : Object;
    }
}
