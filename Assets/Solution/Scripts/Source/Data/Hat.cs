using System;
using System.Collections.Generic;

namespace Greg.Data
{
    [Serializable]
    public class Hat
    {
        public uint HatId;
        public IReadOnlyList<InventorySlot> InventorySlots;
    }
}