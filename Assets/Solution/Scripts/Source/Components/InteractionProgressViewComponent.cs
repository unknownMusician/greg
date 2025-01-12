using UnityEngine;

namespace Greg.Components
{
    public sealed class InteractionProgressViewComponent : MonoBehaviour
    {
        [field: SerializeField] public Transform LoadingBarHolder { get; set; }
    }
}