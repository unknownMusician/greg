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
            
            eventBus.Subscribe(new CharactersInitialSpawner());
            eventBus.Subscribe(new PlayerObjectSetter());
            
            eventBus.Subscribe(new NpcPathVisualizer());
            
            eventBus.Subscribe(new HeightDepthApplier());
            
            eventBus.Subscribe(new StealablesHolderInitializer());
            eventBus.Subscribe(new NpcWalker());
            
            eventBus.Subscribe(new PlayerMoveInputReader());
            eventBus.Subscribe(new PlayerStealInputReader());
            eventBus.Subscribe(new HatSwapInputReader());
            
            eventBus.Subscribe(new PlayerMover());
            eventBus.Subscribe(new PlayerStealer());
            eventBus.Subscribe(new PlayerInnocentStealer());

            eventBus.Subscribe(new InventoryChangeTracker());
            eventBus.Subscribe(new InventoryViewUpdater());
            eventBus.Subscribe(new InventoryPriceTextUpdater());
            eventBus.Subscribe(new GamePauser());
            eventBus.Subscribe(new GamePausedWindowEnabler());
            eventBus.Subscribe(new SpeedrunTimeUpdater());
            eventBus.Subscribe(new SpeedrunTimeResetter());
            eventBus.Subscribe(new SpeedrunTimeViewUpdater());
            eventBus.Subscribe(new PauseButtonUpdater());
            eventBus.Subscribe(new SoundMuter());
            eventBus.Subscribe(new SoundListenerMuter());
            eventBus.Subscribe(new MuteButtonUpdater());
            
            eventBus.Subscribe(new GuardsLookDirectionUpdater());
            eventBus.Subscribe(new GuardsLook());
            eventBus.Subscribe(new GuardsDetectedPlayerNotifier());
            eventBus.Subscribe(new GuardStateVisualizer());
            eventBus.Subscribe(new GuardLookAreaVisualizer());
            eventBus.Subscribe(new GuardsPlayerLostHandler());
            eventBus.Subscribe(new GuardsPlayerCatcher());
            
            eventBus.Subscribe(new InnocentInitializer());
            eventBus.Subscribe(new CharacterHatInitializer());
            eventBus.Subscribe(new CharacterCrowdSfxTypeInitializer());
            eventBus.Subscribe(new StartScreenDisabler());
            
            eventBus.Subscribe(new InteractionTargetTriggerEnterHandler());
            eventBus.Subscribe(new InteractionTargetTriggerExitHandler());
            eventBus.Subscribe(new InnocentInteractionStateChangeHandler());
            
            eventBus.Subscribe(new PathFindingGridInitializer());
            eventBus.Subscribe(new PathFindingInitializer());
            eventBus.Subscribe(new PathFindingVisualizer());
            
            eventBus.Subscribe(new InnocentHatSwapper());
            eventBus.Subscribe(new SafemanHatSwapper());
            
            eventBus.Subscribe(new LevelMoneyStorageInitializer());
            eventBus.Subscribe(new LevelMoneyMaxValueUpdater());
            eventBus.Subscribe(new LevelMoneyStorageVisualUpdater());

            eventBus.Subscribe(new GameEnder());
            eventBus.Subscribe(new GameRestarter());
            eventBus.Subscribe(new DoorExit());
        }
    }
}
