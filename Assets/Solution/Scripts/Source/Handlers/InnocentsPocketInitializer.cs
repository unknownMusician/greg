using System.Collections.Generic;
using System.Linq;
using AreYouFruits.Collections;
using AreYouFruits.Events;
using AreYouFruits.Nullability;
using Greg.Components;
using Greg.Data;
using Greg.Events;
using Greg.Global.Holders;
using Greg.Holders;
using UnityEngine;

namespace Greg.Handlers
{
    public sealed partial class InnocentsPocketInitializer
    {
        [EventHandler]
        private static void Handle(
            StartEvent _,
            BuiltDataHolder builtDataHolder,
            ComponentsResource componentsResource
        )
        {
            var innocentPocketPossibleItems = builtDataHolder.ItemSettings.Where(settings => settings.SourceType == ItemSourceType.Innocent).ToList();

            foreach (var gameObject in componentsResource.Get<PocketComponent>())
            {
                var pocketComponent = gameObject.GetComponent<PocketComponent>();

                var value = Random.value;
                
                if (value <= builtDataHolder.PocketItemSpawnProbability)
                {
                    pocketComponent.StoredItemId = innocentPocketPossibleItems.GetRandomElement<ItemSettings, List<ItemSettings>>().Id;
                }
                else
                {
                    pocketComponent.StoredItemId = Optional.None<uint>();
                }

                Debug.Log($"[InnocentsPocketInitializer] {gameObject.name}={pocketComponent.StoredItemId.ToString()}");
            }
        }
    }
}