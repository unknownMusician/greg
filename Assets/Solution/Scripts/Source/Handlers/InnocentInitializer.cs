using System.Collections.Generic;
using System.Linq;
using AreYouFruits.Collections;
using AreYouFruits.Events;
using AreYouFruits.Nullability;
using Greg.Components;
using Greg.Data;
using Greg.Events;
using Greg.Global.Holders;
using UnityEngine;

namespace Greg.Handlers
{
    public sealed partial class InnocentInitializer
    {
        [EventHandler]
        private static void Handle(
            CharacterSpawnedEvent @event,
            BuiltDataHolder builtDataHolder
        )
        {
            if (@event.CharacterType != CharacterType.Innocent)
            {
                return;
            }
            
            var pocketComponent = @event.GameObject.GetComponent<PocketComponent>();
            
            var value = Random.value;
                
            if (value <= builtDataHolder.PocketItemSpawnProbability)
            {
                var pocketPossibleItems = builtDataHolder.ItemSettings.Where(settings => settings.SourceType == ItemSourceType.Innocent).ToList();
                pocketComponent.StoredItemId = pocketPossibleItems.GetRandomElement<ItemSettings, List<ItemSettings>>().Id;
            }
            else
            {
                pocketComponent.StoredItemId = Optional.None<uint>();
            }
        }
    }
}