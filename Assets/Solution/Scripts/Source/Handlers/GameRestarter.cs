using AreYouFruits.Events;
using Greg.Events;
using UnityEngine.SceneManagement;

namespace Greg.Handlers
{
    public sealed partial class GameRestarter
    {
        [EventHandler]
        private static void Handle(
            RestartButtonClickedEvent _
        )
        {
            SceneManager.LoadScene(0);
        }
    }
}