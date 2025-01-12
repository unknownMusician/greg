using AreYouFruits.Events;
using Greg.Components;
using Greg.Data;
using Greg.Events;
using Greg.Global.Holders;
using UnityEngine;

namespace Greg.Handlers
{
    public sealed partial class ArtifactsInitializer
    {
        [EventHandler]
        private static void Handle(
            StartEvent _,
            BuiltDataHolder builtDataHolder
        )
        {
            var pocketComponents = Object.FindObjectsByType<PocketComponent>(FindObjectsSortMode.None);
            foreach (var pocketComponent in pocketComponents)
            {
                var interactionTargetComponent = pocketComponent.GetComponent<InteractionTargetComponent>();
                
                if (interactionTargetComponent.InteractionTargetType != InteractionTargetType.Artifact)
                {
                    continue;   
                }

                pocketComponent.StoredItemId = pocketComponent.InitialItemId;
                
            }
        }
    }
}