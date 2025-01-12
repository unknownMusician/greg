using AreYouFruits.Events;
using Greg.Components;

namespace Greg.Events
{
    public struct PlayerUntriggeredInteractionTargetEvent : IEvent
    {
        public InteractionTargetComponent InteractionTargetComponent { get; set; }
    }
}