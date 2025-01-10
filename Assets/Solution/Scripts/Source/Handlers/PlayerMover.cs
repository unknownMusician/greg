using AreYouFruits.Events;
using Greg.Events;
using Greg.Global.Holders;
using UnityEngine;

namespace Solution.Scripts.Source.Handlers
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
            sceneDataHolder.Player.transform.position += @event.Direction * builtDataHolder.PlayerSpeed * Time.deltaTime;
        }
    }
}