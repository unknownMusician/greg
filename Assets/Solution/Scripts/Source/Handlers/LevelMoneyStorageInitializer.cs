using AreYouFruits.Events;
using Greg.Events;
using Greg.Global.Api;
using Greg.Holders;

namespace Greg.Handlers
{
    public sealed partial class LevelMoneyStorageInitializer
    {
        [EventHandler]
        private static void Handle(
            StartEvent _,
            LevelMoneyStorageHolder levelMoneyStorageHolder
        )
        {
            levelMoneyStorageHolder.MaxMoneyValue = 0;
            levelMoneyStorageHolder.CollectedMoneyValue = 0;
            EventContext.Bus.Invoke(new LevelMoneyValueChangeEvent());
        }
    }
}