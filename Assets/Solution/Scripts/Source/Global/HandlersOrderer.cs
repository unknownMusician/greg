using AreYouFruits.Events;
using Greg.Events;
using Greg.Global.Handlers;
using Greg.Utils.TagSearcher;
using Greg.Handlers;
using UnityEngine;

namespace Greg.Global
{
    [ScriptTag(ArchitectureTag.Global)]
    public sealed class HandlersOrderer : MonoBehaviour, IHandlerOrderer
    {
        public void Order(GroupGraphOrderer orderer)
        {
            OrderStart(orderer);
            OrderUpdate(orderer);
            OrderCharacterSpawnedEvent(orderer);
        }

        private static void OrderStart(GroupGraphOrderer orderer)
        {
            var eventOrderer = orderer.ForEvent<StartEvent>();

            eventOrderer.Order<InitializePredefinedResources>().Before<InitializeResources>();
            eventOrderer.Order<InitializePredefinedResources>().Before<CharactersInitialSpawner>();
            eventOrderer.Order<InitializeResources>().Before<CharactersInitialSpawner>();
            eventOrderer.Order<InitializeResources>().Before<StealablesHolderInitializer>();
            eventOrderer.Order<InitializeResources>().Before<PathFindingGridInitializer>();
            eventOrderer.Order<InitializeResources>().Before<LevelMoneyStorageInitializer>();
            eventOrderer.Order<InitializeResources>().Before<PathFindingInitializer>();
            eventOrderer.Order<InitializeResources>().Before<SpeedrunTimeResetter>();
            eventOrderer.Order<InitializeResources>().Before<ArtifactsInitializer>();
            
            eventOrderer.Order<LevelMoneyStorageInitializer>().Before<CharactersInitialSpawner>();
        }

        private static void OrderUpdate(GroupGraphOrderer orderer)
        {
            var eventOrderer = orderer.ForEvent<UpdateEvent>();
            
            eventOrderer.Order<SpeedrunTimeUpdater>().Before<SpeedrunTimeViewUpdater>();
        }
        
        private static void OrderCharacterSpawnedEvent(GroupGraphOrderer orderer)
        {
            var eventOrderer = orderer.ForEvent<CharacterSpawnedEvent>();

            eventOrderer.Order<InnocentInitializer>().Before<LevelMoneyMaxValueUpdater>();
        }
    }
}
