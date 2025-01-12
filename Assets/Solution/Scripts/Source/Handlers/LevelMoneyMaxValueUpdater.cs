using System.Linq;
using AreYouFruits.Events;
using Greg.Components;
using Greg.Data;
using Greg.Events;
using Greg.Global.Api;
using Greg.Global.Holders;
using Greg.Holders;

namespace Greg.Handlers
{
    public sealed partial class LevelMoneyMaxValueUpdater
    {
        [EventHandler]
        private static void Handle(
            CharacterSpawnedEvent @event,
            BuiltDataHolder builtDataHolder,
            LevelMoneyStorageHolder levelMoneyStorageHolder
        )
        {
            if (@event.CharacterType is not CharacterType.Innocent)
            {
                return;
            }

            if (!@event.GameObject.TryGetComponent<PocketComponent>(out var pocketComponent) || !pocketComponent.StoredItemId.IsInitialized)
            {
                return;
            }
            
            var storedItemId = pocketComponent.StoredItemId.GetOrThrow();
            var itemSettings = builtDataHolder.ItemSettings.First(settings => settings.Id == storedItemId);
            levelMoneyStorageHolder.MaxMoneyValue += itemSettings.Price;
            
            EventContext.Bus.Invoke(new LevelMoneyValueChangeEvent());
        }
    }
}