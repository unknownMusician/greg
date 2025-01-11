using AreYouFruits.Events;
using Greg.Events;
using Greg.Global.Api;
using Greg.Holders;

namespace Greg.Handlers
{
    public sealed partial class SoundMuter
    {
        [EventHandler]
        private static void Handle(
            MuteButtonClickedEvent _,
            IsSoundMutedHolder isSoundMutedHolder
        )
        {
            isSoundMutedHolder.IsSoundMuted = !isSoundMutedHolder.IsSoundMuted;

            EventContext.Bus.Invoke(new IsSoundMutedChangedEvent());
        }
    }
}