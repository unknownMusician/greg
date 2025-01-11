using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Greg.Components
{
    public sealed class GuardLookAreaComponent : MonoBehaviour
    {
        [field: SerializeField] public Light2D LookArea { get; private set; }
    }
}