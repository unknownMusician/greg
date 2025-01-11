using AreYouFruits.Events;
using Greg.Components;
using Greg.Data;
using Greg.Events;

namespace Greg.Handlers
{
    public sealed partial class GuardsDetectedPlayerNotifier
    {
        [EventHandler]
        private static void Handle(
            GuardDetectedPlayerEvent @event
            )
        {
            @event.Guard.GetComponent<GuardStateComponent>().State = GuardStateType.Aggressive;
        }
    }
}