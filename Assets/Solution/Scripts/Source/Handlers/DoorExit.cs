using AreYouFruits.Events;
using Greg.Components;
using Greg.Events;
using Greg.Global.Api;
using Greg.Global.Holders;
using Greg.Holders;
using UnityEngine;

namespace Greg.Handlers
{
    public sealed partial class DoorExit
    {
        [EventHandler]
        private static void Handle(
            UpdateEvent _,
            PlayerObjectHolder playerObjectHolder,
            BuiltDataHolder builtDataHolder
            )
        {
            if (!Input.GetKeyDown(KeyCode.E))
            {
                return;
            }

            var colliders = Physics2D.OverlapCircleAll(playerObjectHolder.GameObject.transform.position, builtDataHolder.ExitDistance);

            foreach (var collider in colliders)
            {
                if (collider.GetComponentInParent<DoorComponent>() != null)
                {
                    EventContext.Bus.Invoke(new GameEndedEvent());
                }
            }
        }
    }
}