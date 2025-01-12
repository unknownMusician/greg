using AreYouFruits.Events;
using Greg.Events;

namespace Greg.Handlers
{
    public sealed partial class GameRestarter
    {
        [EventHandler]
        private static void Handle(
            RestartButtonClickedEvent _
            )
        {
            // TODO : Implement game restart
        }
    }
}