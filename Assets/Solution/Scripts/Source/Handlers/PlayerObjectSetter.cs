using AreYouFruits.Events;
using Greg.Data;
using Greg.Events;
using Greg.Holders;

namespace Greg.Handlers
{
    public sealed partial class PlayerObjectSetter
    {
        [EventHandler]
        private static void Handle(
            CharacterSpawnedEvent @event,
            PlayerObjectHolder playerObjectHolder
        )
        {
            if (@event.CharacterType == CharacterType.Player)
            {
                playerObjectHolder.GameObject = @event.GameObject;
            }
        }
    }
}