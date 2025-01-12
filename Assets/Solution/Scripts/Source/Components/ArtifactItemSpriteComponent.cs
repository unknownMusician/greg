using UnityEngine;

namespace Greg.Components
{
    public sealed class ArtifactItemSpriteComponent : MonoBehaviour
    {
        [field: SerializeField] public SpriteRenderer Icon { get; set; }
    }
}