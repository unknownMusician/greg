using System.Collections.Generic;
using UnityEngine;

namespace Solution.Scripts.Source.Holders
{
    public sealed class InventoryItemsHolder
    {
        public int InventoryChangeLastFrame { get; set; }
        
        private readonly List<uint> items = new();
        public IReadOnlyList<uint> Items => items;
        
        public void Add(uint itemId)
        {
            items.Add(itemId);
            InventoryChangeLastFrame = Time.frameCount;
        }
    }
}