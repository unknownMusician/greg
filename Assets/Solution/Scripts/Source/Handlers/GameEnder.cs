using AreYouFruits.Events;
using Greg.Events;
using Greg.Global.Holders;

namespace Greg.Handlers
{
    public sealed partial class GameEnder
    {
        [EventHandler]
        private static void Handle(
            GameEndedEvent _,
            SceneDataHolder sceneDataHolder
        )
        {
            sceneDataHolder.ResultWindow.SetActive(true);
        }
    }
}