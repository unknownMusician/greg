using AreYouFruits.Events;
using Greg.Events;
using Greg.Global.Api;
using Greg.Holders;

namespace Greg.Handlers
{
    public sealed partial class InteractionTargetTriggerEnterHandler
    {
        [EventHandler]
        private static void Handle(
            PlayerTriggeredInteractionTargetEvent @event,
            PlayerInteractionTargetHolder playerInteractionTargetHolder
        )
        {
            if (playerInteractionTargetHolder.Value.IsInitialized)
            {
                return;
            }
            
            playerInteractionTargetHolder.Value = @event.InteractionTargetComponent;
                
            EventContext.Bus.Invoke(new InteractionTargetStateChangedEvent());
        }
    }
}