using AreYouFruits.Events;
using Greg.Events;
using Greg.Global.Api;
using Solution.Scripts.Source.Holders;
using UnityEngine;

namespace Solution.Scripts.Source.Handlers
{
    public sealed partial class InventoryChangeTracker
    {
        [EventHandler]
        private static void Handle(
            UpdateEvent _,
            InventoryItemsHolder inventoryItemsHolder
            )
        {
            if (inventoryItemsHolder.InventoryChangeLastFrame == Time.frameCount - 1)
            {
                EventContext.Bus.Invoke(new InventoryChangedEvent());
            }
        }
    }
}