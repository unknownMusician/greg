using System;
using AreYouFruits.Nullability;

namespace Greg.Data
{
    [Serializable]
    public struct InventorySlot
    {
        public Optional<uint> StoredItemId;
    }
}