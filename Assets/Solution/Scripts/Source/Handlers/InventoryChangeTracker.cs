using AreYouFruits.Events;
using Greg.Events;
using Greg.Global.Api;
using Greg.Holders;
using UnityEngine;

namespace Greg.Handlers
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