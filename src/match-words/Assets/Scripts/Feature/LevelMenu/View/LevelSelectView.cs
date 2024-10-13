using System.Collections.Generic;
using Infrastructure.Progress;
using UnityEngine;
using Zenject;

namespace Feature.LevelMenu.View
{
    public class LevelSelectView : MonoBehaviour
    {
        [SerializeField] private List<LevelButtonView> _levelButtons;

        [Inject]
        private void Construct(IGameProgressService gameProgressService)
        {

        }
    }
}
