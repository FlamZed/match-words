using Cysharp.Threading.Tasks;

namespace Infrastructure.View
{
    public interface IView
    {
        UniTask Show();
        UniTask Hide();
    }
}
