using AreYouFruits.Nullability;
using UnityEngine;

namespace Greg.Components
{
    public sealed class PocketComponent : MonoBehaviour
    {
        [field:SerializeField] public uint InitialItemId { get; set; }
        public Optional<uint> StoredItemId { get; set; }
    }
}