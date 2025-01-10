using UnityEngine;
using UnityEngine.UI;

namespace Solution.Scripts.Source.Components
{
    public sealed class InventoryCellComponent : MonoBehaviour
    {
        [field: SerializeField] public Image Icon { get; private set; }
    }
}