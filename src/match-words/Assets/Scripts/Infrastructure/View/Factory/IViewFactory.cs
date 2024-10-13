using Cysharp.Threading.Tasks;

namespace Infrastructure.View.Factory
{
    public interface IViewFactory
    {
        UniTask<IView> Instantiate(ViewType viewType);
    }
}