using System.Linq;
using AreYouFruits.Events;
using Greg.Components;
using Greg.Events;
using Greg.Global.Holders;
using Greg.Holders;
using Greg.Utils;

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
            var sum = inventoryItemsHolder.GetSum(builtDataHolder);
            var formattedSum = $"{sum}";
            
            foreach (var gameObject in componentsResource.Get<InventoryMoneyTextComponent>())
            {
                gameObject.GetComponent<InventoryMoneyTextComponent>().Text.text = formattedSum;
            }
        }
    }
}