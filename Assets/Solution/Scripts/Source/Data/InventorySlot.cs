using System;
using AreYouFruits.Nullability;

namespace Greg.Data
{
    [Serializable]
    public class InventorySlot
    {
        public Optional<uint> StoredItemId;
    }
}