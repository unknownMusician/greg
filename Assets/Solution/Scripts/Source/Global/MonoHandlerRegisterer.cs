using AreYouFruits.Events;
using UnityEngine;

namespace Greg.Global
{
    public abstract class MonoHandlerRegisterer<TEvent> : MonoBehaviour, IHandlerRegisterer, IEventHandler<TEvent>
        where TEvent : IEvent
        // todo: Temp. Remove when ECS arrives.
    {
        public void Register(EventBus eventBus)
        {
            eventBus.Subscribe(this);
        }

        public abstract void Handle(TEvent @event);
    }
}
