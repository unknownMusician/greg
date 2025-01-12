using UnityEngine;

namespace Greg.Components
{
    public sealed class ArtifactItemViewComponent : MonoBehaviour
    {
        [field: SerializeField] public GameObject HintHolder { get; set; }
        [field: SerializeField] public SpriteRenderer Icon { get; set; }
    }
}