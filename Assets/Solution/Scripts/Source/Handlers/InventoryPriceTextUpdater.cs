using System.Linq;
using AreYouFruits.Events;
using Greg.Components;
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
            ComponentsResource componentsResource,
            BuiltDataHolder builtDataHolder
        )
        {
            var sum = inventoryItemsHolder.Items
                .Sum(itemId => builtDataHolder.ItemSettings.First(s => s.Id == itemId).Price);
            var formattedSum = $"{sum}";
            
            foreach (var gameObject in componentsResource.Get<InventoryMoneyTextComponent>())
            {
                gameObject.GetComponent<InventoryMoneyTextComponent>().Text.text = formattedSum;
            }
        }
    }
}