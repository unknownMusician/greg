using AreYouFruits.Events;
using Greg.Components;
using Greg.Events;
using Greg.Global.Holders;
using Greg.Holders;
using TMPro;

namespace Greg.Handlers
{
    public sealed partial class PauseButtonUpdater
    {
        [EventHandler]
        private static void Handle(
            IsGamePausedChangedEvent _,
            SceneDataHolder sceneDataHolder,
            IsGamePausedHolder isGamePausedHolder
        )
        {
            sceneDataHolder.PauseButton.GetComponent<PauseButtonView>().Text.text = isGamePausedHolder.IsPaused 
                ? "UNPAUSE" 
                : "PAUSE";
        }
    }
}