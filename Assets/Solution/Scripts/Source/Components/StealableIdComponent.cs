using UnityEngine;

namespace Solution.Scripts.Source.Components
{
    public sealed class StealableIdComponent : MonoBehaviour
    {
        [field: SerializeField] public uint Id { get; private set; }
    }
}