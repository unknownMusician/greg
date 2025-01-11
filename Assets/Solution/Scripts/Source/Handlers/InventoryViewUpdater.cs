using System.Linq;
using AreYouFruits.Events;
using Greg.Components;
using Greg.Events;
using Greg.Global.Holders;
using Greg.Holders;
using UnityEngine;

namespace Greg.Handlers
{
    public sealed partial class InventoryViewUpdater
    {
        [EventHandler]
        private static void Handle(
            InventoryChangedEvent _,
            ComponentsResource componentsResource,
            InventoryItemsHolder inventoryItemsHolder,
            BuiltDataHolder builtDataHolder
            )
        {
            var inventoryCellComponents = componentsResource.Get<InventoryCellComponent>().ToList();
            for (var i = 0; i < inventoryCellComponents.Count; i++)
            {
                var inventoryCell = inventoryCellComponents[i].GetComponent<InventoryCellComponent>();

                if (i >= inventoryItemsHolder.Items.Count)
                {
                    inventoryCell.Icon.sprite = null;
                    inventoryCell.Icon.color = Color.clear;
                    return;
                }

                var settings = builtDataHolder.ItemSettings.First(s => s.Id == inventoryItemsHolder.Items[i]);
                inventoryCell.Icon.sprite = settings.Icon;
                inventoryCell.Icon.color = Color.white;
            }
        }
    }
}