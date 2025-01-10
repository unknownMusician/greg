using AreYouFruits.Events;
using Greg.Events;
using Greg.Utils.TagSearcher;

namespace Greg.Global
{
    [ScriptTag(ArchitectureTag.Handler)]
    public sealed class InitializeResources : IEventHandler<StartEvent>
    {
        public void Handle(StartEvent @event)
        {
        }
    }
}