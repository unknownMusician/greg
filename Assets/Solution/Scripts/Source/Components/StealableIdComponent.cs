using UnityEngine;

namespace Greg.Components
{
    public sealed class StealableIdComponent : MonoBehaviour
    {
        [field: SerializeField] public uint Id { get; private set; }
    }
}