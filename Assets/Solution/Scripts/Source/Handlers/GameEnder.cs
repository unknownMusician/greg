using AreYouFruits.Events;
using Greg.Events;
using Greg.Global.Holders;
using Greg.Holders;
using Greg.Utils;

namespace Greg.Handlers
{
    public sealed partial class GameEnder
    {
        [EventHandler]
        private static void Handle(
            GameEndedEvent _,
            IsGameEndedHolder isGameEndedHolder,
            SceneDataHolder sceneDataHolder,
            StartRealTimeHolder startRealTimeHolder,
            LevelMoneyStorageHolder levelMoneyStorageHolder
        )
        {
            isGameEndedHolder.IsGameEnded = true;
            
            sceneDataHolder.ResultWindow.SetActive(true);
            sceneDataHolder.ResultMoneyText.text = $"{levelMoneyStorageHolder.CollectedMoneyValue}";
            sceneDataHolder.ResultTimeText.text = $"{startRealTimeHolder.GetTimer()}";
        }
    }
}