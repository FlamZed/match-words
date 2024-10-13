using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Feature.PrintMachine.View.List.Factory
{
    public interface IListWordFactory
    {
        UniTask<GameObject> Produce(Transform parent);
    }
}