using AreYouFruits.Events;
using Greg.Events;
using Greg.Global.Holders;
using Greg.Holders;
using UnityEngine;

namespace Greg.Handlers
{
    public sealed partial class PlayerMover
    {
        [EventHandler]
        private static void Handle(
            PlayerMoveInputEvent @event,
            PlayerObjectHolder playerObjectHolder,
            BuiltDataHolder builtDataHolder
        )
        {
            playerObjectHolder.GameObject.GetComponent<Rigidbody2D>().linearVelocity = @event.Direction * builtDataHolder.PlayerSpeed;
        }
    }
}