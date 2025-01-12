using UnityEngine;

namespace Greg.Components
{
    public sealed class CustomizationRendererComponent : MonoBehaviour
    {
        [field:SerializeField] public SpriteRenderer HeadSpriteRenderer { get; set; }
        [field:SerializeField] public SpriteRenderer BodySpriteRenderer { get; set; }
        [field:SerializeField] public SpriteRenderer LegsSpriteRenderer { get; set; }
    }
}