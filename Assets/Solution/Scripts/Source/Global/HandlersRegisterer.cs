using AreYouFruits.Events;
using Greg.Utils.TagSearcher;
using Solution.Scripts.Source.Handlers;
using UnityEngine;

namespace Greg.Global
{
    [ScriptTag(ArchitectureTag.Global)]
    public sealed class HandlersRegisterer : MonoBehaviour, IHandlerRegisterer
    {
        public void Register(EventBus eventBus)
        {
            eventBus.Subscribe(new InitializeResources());
            
            eventBus.Subscribe(new StealablesHolderInitializer());
            
            eventBus.Subscribe(new PlayerMoveInputReader());
            eventBus.Subscribe(new PlayerStealInputReader());
            
            eventBus.Subscribe(new PlayerMover());
            eventBus.Subscribe(new PlayerRotator());
            eventBus.Subscribe(new PlayerStealer());
        }
    }
}
