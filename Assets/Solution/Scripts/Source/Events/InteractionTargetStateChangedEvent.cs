using AreYouFruits.Events;
using Greg.Components;

namespace Greg.Events
{
    public struct InteractionTargetStateChangedEvent : IEvent
    {
        public InteractionTargetComponent InteractionTargetComponent { get; set; }
    }
}