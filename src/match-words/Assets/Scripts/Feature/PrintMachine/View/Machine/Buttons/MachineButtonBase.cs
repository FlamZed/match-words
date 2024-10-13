using UnityEngine;
using UnityEngine.UI;

namespace Feature.PrintMachine.View
{
    public abstract class MachineButtonBase : MonoBehaviour
    {
        [SerializeField] protected Button button;

        [Space]
        [SerializeField] protected Image image;

        [Space, Header("Sprites")]
        [SerializeField] protected Sprite selectedSprite;
        [SerializeField] protected Sprite unselectedSprite;
    }
}
