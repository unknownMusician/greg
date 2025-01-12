using AreYouFruits.Events;
using Greg.Events;
using Greg.Holders;

namespace Greg.Handlers
{
    public sealed partial class InteractionTargetStateChangeHandler
    {
        [EventHandler]
        private static void Handle(
            InteractionTargetStateChangedEvent _,
            PlayerInteractionTargetHolder playerInteractionTargetHolder
        )
        {
            UnityEngine.Debug.Log($"[InteractionTargetTriggerEnterHandler] {playerInteractionTargetHolder.Value.ToString()}");
        }
    }
}