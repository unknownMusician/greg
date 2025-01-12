using AreYouFruits.Events;
using Greg.Components;

namespace Greg.Events
{
    public struct InteractionTargetDeactivatedEvent : IEvent
    {
        public InteractionTargetComponent InteractionTargetComponent { get; set; }
    }
}