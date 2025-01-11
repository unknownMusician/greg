using AreYouFruits.Events;
using Greg.Utils.TagSearcher;
using Greg.Handlers;
using UnityEngine;

namespace Greg.Global
{
    [ScriptTag(ArchitectureTag.Global)]
    public sealed class HandlersRegisterer : MonoBehaviour, IHandlerRegisterer
    {
        public void Register(EventBus eventBus)
        {
            eventBus.Subscribe(new InitializeResources());
            
            eventBus.Subscribe(new NpcPathVisualizer());
            
            eventBus.Subscribe(new StealablesHolderInitializer());
            eventBus.Subscribe(new NpcWalker());
            
            eventBus.Subscribe(new PlayerMoveInputReader());
            eventBus.Subscribe(new PlayerStealInputReader());
            
            eventBus.Subscribe(new PlayerMover());
            eventBus.Subscribe(new PlayerStealer());

            eventBus.Subscribe(new InventoryChangeTracker());
            eventBus.Subscribe(new InventoryViewCreator());
            eventBus.Subscribe(new InventoryViewUpdater());
            
            eventBus.Subscribe(new GuardsLookDirectionUpdater());
            eventBus.Subscribe(new GuardsLook());
            eventBus.Subscribe(new GuardsDetectedPlayerNotifier());
            eventBus.Subscribe(new GuardsLookAreaVisualizer());
        }
    }
}
