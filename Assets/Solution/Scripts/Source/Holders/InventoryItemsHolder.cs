using System.Collections.Generic;
using UnityEngine;

namespace Greg.Holders
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
        
        public void AddRange(IEnumerable<uint> itemIds)
        {
            foreach (var itemId in itemIds)
            {
                items.Add(itemId);
            }
            InventoryChangeLastFrame = Time.frameCount;
        }
        
        public void Clear()
        {
            items.Clear();
            InventoryChangeLastFrame = Time.frameCount;
        }
    }
}