using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Solution.Scripts.Source.Components
{
    public sealed class GuardLookAreaComponent : MonoBehaviour
    {
        [field: SerializeField] public Light2D LookArea { get; private set; }
    }
}