using AreYouFruits.Events;
using Greg.Events;
using Greg.Global.Holders;
using Greg.Holders;
using Greg.Utils;

namespace Greg.Handlers
{
    public sealed partial class GameEnder
    {
        [EventHandler]
        private static void Handle(
            GameEndedEvent _,
            SceneDataHolder sceneDataHolder,
            InventoryItemsHolder inventoryItemsHolder,
            BuiltDataHolder builtDataHolder,
            StartRealTimeHolder startRealTimeHolder
        )
        {
            sceneDataHolder.ResultWindow.SetActive(true);
            sceneDataHolder.ResultMoneyText.text = $"MONEY: {inventoryItemsHolder.GetSum(builtDataHolder)}";
            sceneDataHolder.ResultTimeText.text = $"TIME: {startRealTimeHolder.GetTimer()}";
        }
    }
}