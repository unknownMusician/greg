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
    public sealed partial class PlayerStealInputReader
    {
        [EventHandler]
        private static void Handle(
            UpdateEvent _,
            IsGameEndedHolder isGameEndedHolder,
            StealProgressHolder stealProgressHolder,
            PlayerInteractionTargetHolder playerInteractionTargetHolder,
            BuiltDataHolder builtDataHolder
            )
        {
            if (isGameEndedHolder.IsGameEnded || !playerInteractionTargetHolder.Value.IsInitialized)
            {
                return;
            }
            
            var interactionTargetComponent = playerInteractionTargetHolder.Value.GetOrThrow();

            if (interactionTargetComponent.InteractionTargetType is not (InteractionTargetType.Innocent or InteractionTargetType.Artifact))
            {
                return;
            }
            
            if (!Input.GetKey(KeyCode.F))
            {
                stealProgressHolder.StealingProgressNormalized = 0f;
                interactionTargetComponent.GetComponent<InteractionProgressViewComponent>().LoadingBarHolder.localScale = new Vector3(1f - stealProgressHolder.StealingProgressNormalized, 1f, 1f);
                return;
            }
            
            Debug.Log($"Steal progress: {stealProgressHolder.StealingProgressNormalized}");
            
            if (stealProgressHolder.StealingProgressNormalized >= 1f)
            {
                EventContext.Bus.Invoke(new PlayerStealInputEvent());
                stealProgressHolder.StealingProgressNormalized = 0f;
                return;
            }
            
            var interactionProgressViewComponent = interactionTargetComponent.GetComponent<InteractionProgressViewComponent>();
            stealProgressHolder.StealingProgressNormalized += Time.unscaledDeltaTime / builtDataHolder.StealDurationInSeconds;

            interactionProgressViewComponent.LoadingBarHolder.localScale = new Vector3(1f - stealProgressHolder.StealingProgressNormalized, 1f, 1f);
        }
    }
}