using TMPro;
using UnityEngine;

namespace Greg.Components
{
    public sealed class PauseButtonView : MonoBehaviour
    {
        [field: SerializeField] public TextMeshProUGUI Text { get; private set; }
    }
}