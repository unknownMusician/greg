using AreYouFruits.Events;
using Greg.Components;

namespace Greg.Events
{
    public struct PlayerTriggeredInteractionTargetEvent : IEvent
    {
        public InteractionTargetComponent InteractionTargetComponent { get; set; }
    }
}