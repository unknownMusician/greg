using System.Linq;
using AreYouFruits.Events;
using Greg.Components;
using Greg.Data;
using Greg.Events;
using Greg.Global.Holders;
using Greg.Holders;

namespace Greg.Handlers
{
    public sealed partial class InnocentInteractionStateChangeHandler
    {
        [EventHandler]
        private static void Handle(
            InteractionTargetStateChangedEvent @event,
            PlayerInteractionTargetHolder playerInteractionTargetHolder,
            BuiltDataHolder builtDataHolder
        )
        {
            UnityEngine.Debug.Log($"[InteractionTargetTriggerEnterHandler] {playerInteractionTargetHolder.Value.ToString()}");

            if (@event.InteractionTargetComponent.InteractionTargetType != InteractionTargetType.Innocent)
            {
                return;
            }

            var pocketComponent = @event.InteractionTargetComponent.GetComponent<PocketComponent>();
            var innocentItemViewComponent = @event.InteractionTargetComponent.GetComponent<InnocentItemViewComponent>();

            if (playerInteractionTargetHolder.Value.IsInitialized)
            {
                if (!pocketComponent.StoredItemId.IsInitialized)
                {
                    return;
                }

                var storedItemId = pocketComponent.StoredItemId.GetOrThrow();
                var itemSettings = builtDataHolder.ItemSettings.First(settings => settings.Id == storedItemId);

                innocentItemViewComponent.Icon.sprite = itemSettings.Icon;
                innocentItemViewComponent.HintHolder.SetActive(true);
            }
            else
            {
                innocentItemViewComponent.Icon.sprite = null;
                innocentItemViewComponent.HintHolder.SetActive(false);
            }
        }
    }
}