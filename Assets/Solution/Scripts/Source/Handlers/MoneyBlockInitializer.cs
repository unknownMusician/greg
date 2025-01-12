using AreYouFruits.Events;
using Greg.Events;
using Greg.Global.Holders;

namespace Greg.Handlers
{
    public sealed partial class MoneyBlockInitializer
    {
        [EventHandler]
        private static void Handle(
            CharacterSpawnedEvent @event,
            SceneDataHolder sceneDataHolder
        )
        {
            
        }
    }
}