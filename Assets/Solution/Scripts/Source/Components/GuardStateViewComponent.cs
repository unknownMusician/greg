using UnityEngine;

namespace Greg.Components
{
    public sealed class GuardStateViewComponent : MonoBehaviour
    {
        [field: SerializeField] public GameObject StateContainer { get; set; }
        [field: SerializeField] public GameObject ExclamationMark { get; set; }
        [field: SerializeField] public SpriteRenderer Icon { get; set; }
    }
}