using System.Linq;
using AreYouFruits.Events;
using Greg.Components;
using Greg.Data;
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
            var inventoryCellComponents = componentsResource.Get<InventoryCellComponent>().OrderBy(o => o.transform.position.x).ToList();
            
            for (var i = 0; i < inventoryCellComponents.Count; i++)
            {
                var inventoryCell = inventoryCellComponents[i].GetComponent<InventoryCellComponent>();

                if (i >= inventoryItemsHolder.Items.Count)
                {
                    SetItemIcon(inventoryCell, null);
                    continue;
                }

                var settings = builtDataHolder.ItemSettings.First(s => s.Id == inventoryItemsHolder.Items[i]);
                SetItemIcon(inventoryCell, settings.Icon);
            }
        }

        private static void SetItemIcon(InventoryCellComponent inventoryCellComponent, Sprite sprite)
        {
            inventoryCellComponent.Icon.sprite = sprite;
            inventoryCellComponent.Icon.color = sprite == null ? Color.clear : Color.white;
        }
    }
}