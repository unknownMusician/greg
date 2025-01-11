using AreYouFruits.Events;
using Greg.Events;
using Greg.Utils.TagSearcher;
using Greg.Holders;

namespace Greg.Global
{
    [ScriptTag(ArchitectureTag.Handler)]
    public sealed class InitializeResources : IEventHandler<StartEvent>
    {
        public void Handle(StartEvent @event)
        {
            ResourcesLocator.Add(new ComponentsResource());
            
            ResourcesLocator.Add(new StealablesHolder());
            ResourcesLocator.Add(new NpcHolder());
            
            ResourcesLocator.Add(new InventoryItemsHolder());
            ResourcesLocator.Add(new InventoryViewCellsHolder());

            ResourcesLocator.Add(new IsGamePausedHolder());
        }
    }
}