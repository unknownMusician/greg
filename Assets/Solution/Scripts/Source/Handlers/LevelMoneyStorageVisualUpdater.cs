using AreYouFruits.Events;
using Greg.Events;
using Greg.Global.Holders;
using Greg.Holders;

namespace Greg.Handlers
{
    public sealed partial class LevelMoneyStorageVisualUpdater
    {
        [EventHandler]
        private static void Handle(
            LevelMoneyValueChangeEvent _,
            SceneDataHolder sceneDataHolder,
            LevelMoneyStorageHolder levelMoneyStorageHolder
        )
        {
            sceneDataHolder.LevelMoneyTextComponent.Text.text = levelMoneyStorageHolder.MaxMoneyValue.ToString();
            sceneDataHolder.CollectedMoneyTextComponent.Text.text = levelMoneyStorageHolder.CollectedMoneyValue.ToString();
        }
    }
}