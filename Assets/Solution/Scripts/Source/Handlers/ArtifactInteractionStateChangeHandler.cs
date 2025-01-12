using System.Linq;
using AreYouFruits.Events;
using Greg.Components;
using Greg.Data;
using Greg.Events;
using Greg.Global.Holders;
using Greg.Holders;

namespace Greg.Handlers
{
    public sealed partial class ArtifactInteractionStateChangeHandler
    {
        [EventHandler]
        private static void Handle(
            InteractionTargetStateChangedEvent @event,
            PlayerInteractionTargetHolder playerInteractionTargetHolder,
            BuiltDataHolder builtDataHolder
        )
        {
            UnityEngine.Debug.Log($"[ArtifactInteractionStateChangeHandler] {playerInteractionTargetHolder.Value.ToString()}");

            if (@event.InteractionTargetComponent.InteractionTargetType != InteractionTargetType.Artifact)
            {
                return;
            }

            var pocketComponent = @event.InteractionTargetComponent.GetComponent<PocketComponent>();
            var artifactItemViewComponent = @event.InteractionTargetComponent.GetComponent<ArtifactItemViewComponent>();

            if (playerInteractionTargetHolder.Value.IsInitialized)
            {
                if (!pocketComponent.StoredItemId.IsInitialized)
                {
                    return;
                }

                var storedItemId = pocketComponent.StoredItemId.GetOrThrow();
                var itemSettings = builtDataHolder.ItemSettings.First(settings => settings.Id == storedItemId);

                artifactItemViewComponent.Icon.sprite = itemSettings.Icon;
                artifactItemViewComponent.HintHolder.SetActive(true);
            }
            else
            {
                artifactItemViewComponent.Icon.sprite = null;
                artifactItemViewComponent.HintHolder.SetActive(false);
            }
        }
    }
}