using System.Linq;
using AreYouFruits.Events;
using Greg.Components;
using Greg.Data;
using Greg.Events;
using Greg.Global.Api;
using Greg.Global.Holders;
using Greg.Holders;
using UnityEngine;

namespace Greg.Handlers
{
    public sealed partial class LevelMoneyStorageInitializer
    {
        [EventHandler]
        private static void Handle(
            StartEvent _,
            LevelMoneyStorageHolder levelMoneyStorageHolder,
            BuiltDataHolder builtDataHolder
        )
        {
            var pocketComponents = Object.FindObjectsByType<PocketComponent>(FindObjectsSortMode.None);
            
            levelMoneyStorageHolder.MaxMoneyValue = 0;
            foreach (var pocketComponent in pocketComponents)
            {
                var interactionTargetComponent = pocketComponent.GetComponent<InteractionTargetComponent>();
                
                if (interactionTargetComponent.InteractionTargetType != InteractionTargetType.Artifact)
                {
                    continue;
                }

                var itemSettings = builtDataHolder.ItemSettings.First(settings => settings.Id == pocketComponent.StoredItemId);
                levelMoneyStorageHolder.MaxMoneyValue += itemSettings.Price;
            }
            
            levelMoneyStorageHolder.CollectedMoneyValue = 0;
            EventContext.Bus.Invoke(new LevelMoneyValueChangeEvent());
        }
    }
}