using UnityEngine;

namespace Greg.Components
{
    public sealed class HatRendererComponent : MonoBehaviour
    {
        [field:SerializeField] public SpriteRenderer SpriteRenderer { get; set; }
    }
}