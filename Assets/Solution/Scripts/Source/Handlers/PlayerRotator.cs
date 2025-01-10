using AreYouFruits.Events;
using Greg.Events;
using Greg.Global.Holders;
using UnityEngine;

namespace Solution.Scripts.Source.Handlers
{
    public sealed partial class PlayerRotator
    {
        [EventHandler]
        private static void Handle(
            PlayerMoveInputEvent @event,
            SceneDataHolder sceneDataHolder
        )
        {
            var yRotation = @event.Direction.x > 0 ? 180 : 0;
            sceneDataHolder.Player.transform.eulerAngles = Vector3.up * yRotation;
        }
    }
}