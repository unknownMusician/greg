using AreYouFruits.Events;
using Greg.Events;
using Greg.Global.Api;
using Greg.Holders;
using UnityEngine;

namespace Greg.Handlers
{
    public sealed partial class GamePauser
    {
        [EventHandler]
        private static void Handle(
            PauseButtonClickedEvent _,
            IsGamePausedHolder isGamePausedHolder
            )
        {
            isGamePausedHolder.IsPaused = !isGamePausedHolder.IsPaused;
            Time.timeScale = isGamePausedHolder.IsPaused ? 0 : 1;

            EventContext.Bus.Invoke(new IsGamePausedChangedEvent());
        }
    }
}