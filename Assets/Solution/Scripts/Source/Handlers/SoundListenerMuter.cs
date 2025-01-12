using AreYouFruits.Events;
using Greg.Events;
using Greg.Holders;
using UnityEngine;

namespace Greg.Handlers
{
    public sealed partial class SoundListenerMuter
    {
        [EventHandler]
        private static void Handle(
            IsSoundMutedChangedEvent _,
            IsSoundMutedHolder isSoundMutedHolder
        )
        {
            AudioListener.volume = isSoundMutedHolder.IsSoundMuted switch
            {
                true => 0,
                false => 1,
            };
        }
    }
}