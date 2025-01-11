using AreYouFruits.Nullability;
using UnityEngine;

namespace Greg.Components
{
    public sealed class PocketComponent : MonoBehaviour
    {
        public Optional<uint> StoredItemId { get; set; }
    }
}