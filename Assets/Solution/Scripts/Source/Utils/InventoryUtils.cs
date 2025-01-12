using System.Linq;
using Greg.Global.Holders;
using Greg.Holders;

namespace Greg.Utils
{
    public static class InventoryUtils
    {
        public static int GetSum(this InventoryItemsHolder inventoryItemsHolder, BuiltDataHolder builtDataHolder)
        {
            return inventoryItemsHolder.Items
                .Sum(itemId => builtDataHolder.ItemSettings.First(s => s.Id == itemId).Price);
        }
    }
}