using AreYouFruits.Events;
using Greg.Events;
using Greg.Global.Holders;
using Greg.Holders;

namespace Greg.Handlers
{
    public sealed partial class GamePausedWindowEnabler
    {
        [EventHandler]
        private static void Handle(
            IsGamePausedChangedEvent _,
            SceneDataHolder sceneDataHolder,
            IsGamePausedHolder isGamePausedHolder
            )
        {
            sceneDataHolder.GamePausedWindow.SetActive(isGamePausedHolder.IsPaused);
        }
    }
}