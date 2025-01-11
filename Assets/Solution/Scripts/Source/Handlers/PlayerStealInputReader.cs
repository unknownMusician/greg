using AreYouFruits.Events;
using Greg.Events;
using Greg.Global.Api;
using UnityEngine;

namespace Greg.Handlers
{
    public sealed partial class PlayerStealInputReader
    {
        [EventHandler]
        private static void Handle(
            UpdateEvent _
            )
        {
            if (!Input.GetKeyDown(KeyCode.F))
            {
                return;
            }

            EventContext.Bus.Invoke(new PlayerStealInputEvent());
        }
    }
}