using AreYouFruits.Events;
using Greg.Events;
using Greg.Utils.TagSearcher;
using Solution.Scripts.Source.Holders;

namespace Greg.Global
{
    [ScriptTag(ArchitectureTag.Handler)]
    public sealed class InitializeResources : IEventHandler<StartEvent>
    {
        public void Handle(StartEvent @event)
        {
            ResourcesLocator.Add(new StealablesHolder());
            
            ResourcesLocator.Add(new InventoryItemsHolder());
            ResourcesLocator.Add(new InventoryViewCellsHolder());
        }
    }
}