using System.Linq;
using AreYouFruits.Events;
using Greg.Events;
using Greg.Global.Holders;
using Greg.Holders;

namespace Greg.Handlers
{
    public sealed partial class InventoryPriceTextUpdater
    {
        [EventHandler]
        private static void Handle(
            InventoryChangedEvent _,
            InventoryItemsHolder inventoryItemsHolder,
            BuiltDataHolder builtDataHolder,
            SceneDataHolder sceneDataHolder
            )
        {
            var sum = inventoryItemsHolder.Items
                .Sum(itemId => builtDataHolder.ItemSettings.First(s => s.Id == itemId).Price);
            sceneDataHolder.InventoryPriceText.text = $"{sum}";
        }
    }
}