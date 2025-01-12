using UnityEngine;

namespace Greg.Components
{
    public sealed class GuardLookAreaComponent : MonoBehaviour
    {
        [field: SerializeField] public Transform LookArea { get; private set; }
    }
}