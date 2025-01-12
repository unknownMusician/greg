using UnityEngine;
using UnityEngine.UI;

namespace Greg.Components
{
    public sealed class InventoryCellComponent : MonoBehaviour
    {
        [field: SerializeField] public Image Icon { get; private set; }
    }
}