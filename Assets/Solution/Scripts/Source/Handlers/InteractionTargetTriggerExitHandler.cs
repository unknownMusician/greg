using AreYouFruits.Events;
using AreYouFruits.Nullability;
using Greg.Events;
using Greg.Global.Api;
using Greg.Holders;

namespace Greg.Handlers
{
    public sealed partial class InteractionTargetTriggerExitHandler
    {
        [EventHandler]
        private static void Handle(
            PlayerUntriggeredInteractionTargetEvent @event,
            PlayerInteractionTargetHolder playerInteractionTargetHolder
        )
        {
            if (!playerInteractionTargetHolder.Value.IsInitialized || playerInteractionTargetHolder.Value.GetOrThrow() != @event.InteractionTargetComponent)
            {
                return;
            }

            playerInteractionTargetHolder.Value = Optional.None();
                
            EventContext.Bus.Invoke(new InteractionTargetStateChangedEvent()
            {
                InteractionTargetComponent = @event.InteractionTargetComponent,
            });
        }
    }
}