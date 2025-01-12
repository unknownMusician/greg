using AreYouFruits.Events;
using Greg.Events;
using Greg.Global.Api;
using Greg.Holders;
using UnityEngine;

namespace Greg.Handlers
{
    public sealed partial class PlayerStealInputReader
    {
        [EventHandler]
        private static void Handle(
            UpdateEvent _,
            IsGameEndedHolder isGameEndedHolder
            )
        {
            if (isGameEndedHolder.IsGameEnded)
            {
                return;
            }
            
            if (!Input.GetKeyDown(KeyCode.F))
            {
                return;
            }

            EventContext.Bus.Invoke(new PlayerStealInputEvent());
        }
    }
}