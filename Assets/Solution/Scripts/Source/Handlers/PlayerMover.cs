using AreYouFruits.Events;
using Greg.Events;
using Greg.Global.Holders;
using UnityEngine;

namespace Greg.Handlers
{
    public sealed partial class PlayerMover
    {
        [EventHandler]
        private static void Handle(
            PlayerMoveInputEvent @event,
            SceneDataHolder sceneDataHolder,
            BuiltDataHolder builtDataHolder
        )
        {
            sceneDataHolder.Player.GetComponent<Rigidbody2D>().linearVelocity = @event.Direction * builtDataHolder.PlayerSpeed;
        }
    }
}