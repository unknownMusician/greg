using AreYouFruits.Events;

namespace Greg.Global
{
    public interface IHandlerRegisterer
    {
        public void Register(EventBus eventBus);
    }
}
