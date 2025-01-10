using AreYouFruits.Events;

namespace Greg.Global.Api
{
    public static class EventContext
        // todo: Temp. Remove when ECS arrives.
    {
        public static EventBus Bus { get; set; }
    }
}
