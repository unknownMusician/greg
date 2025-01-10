using System.Linq;
using AreYouFruits.Events;
using Greg.Events;
using Greg.Global.Holders;
using Solution.Scripts.Source.Components;
using Solution.Scripts.Source.Holders;
using UnityEngine;

namespace Solution.Scripts.Source.Handlers
{
    public sealed partial class InventoryViewUpdater
    {
        [EventHandler]
        private static void Handle(
            InventoryChangedEvent _,
            InventoryViewCellsHolder inventoryViewCellsHolder,
            InventoryItemsHolder inventoryItemsHolder,
            BuiltDataHolder builtDataHolder
            )
        {
            for (var i = 0; i < inventoryViewCellsHolder.InventoryCells.Count; i++)
            {
                var inventoryCell = inventoryViewCellsHolder.InventoryCells[i].GetComponent<InventoryCellComponent>();

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