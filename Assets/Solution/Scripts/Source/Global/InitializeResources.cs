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
            
            ResourcesLocator.Add(new StartRealTimeHolder());
            ResourcesLocator.Add(new PlayerObjectHolder());
            ResourcesLocator.Add(new StealablesHolder());
            ResourcesLocator.Add(new NpcHolder());
            ResourcesLocator.Add(new StealProgressHolder());
            
            ResourcesLocator.Add(new InventoryItemsHolder());
            ResourcesLocator.Add(new PathFinderHolder());
            ResourcesLocator.Add(new LevelMoneyStorageHolder());

            ResourcesLocator.Add(new IsGamePausedHolder());
            ResourcesLocator.Add(new IsSoundMutedHolder());
            ResourcesLocator.Add(new PlayerInteractionTargetHolder());
            ResourcesLocator.Add(new IsGameEndedHolder());
        }
    }
}