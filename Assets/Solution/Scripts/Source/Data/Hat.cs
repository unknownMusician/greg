using System;
using System.Collections.Generic;

namespace Greg.Data
{
    [Serializable]
    public struct Hat
    {
        public uint HatId;
        public IReadOnlyList<InventorySlot> InventorySlots;
    }
}