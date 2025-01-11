using AreYouFruits.Events;
using Greg.Events;
using Solution.Scripts.Source.Components;

namespace Solution.Scripts.Source.Handlers
{
    public sealed partial class GuardsDetectedPlayerNotifier
    {
        [EventHandler]
        private static void Handle(
            GuardDetectedPlayerEvent @event
            )
        {
            @event.Guard.GetComponent<GuardNotifyComponent>().ExclamationMark.gameObject.SetActive(true);
        }
    }
}