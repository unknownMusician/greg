using AreYouFruits.Events;
using Greg.Events;
using Greg.Global.Holders;
using Greg.Holders;
using UnityEngine;

namespace Greg.Handlers
{
    public sealed partial class InventoryViewCreator
    {
        [EventHandler]
        private static void Handle(
            StartEvent _,
            BuiltDataHolder builtDataHolder,
            SceneDataHolder sceneDataHolder,
            InventoryViewCellsHolder inventoryViewCellsHolder
        )
        {
            for (var i = 0; i < builtDataHolder.InventoryCapacity; i++)
            {
                var inventoryCell = Object.Instantiate(builtDataHolder.InventoryCellPrefab, sceneDataHolder.InventoryCellsParent);
                inventoryViewCellsHolder.InventoryCells.Add(inventoryCell);
            }
        }
    }
}