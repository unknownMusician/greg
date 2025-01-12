using AreYouFruits.Events;
using Greg.Components;
using Greg.Events;
using Greg.Holders;
using Greg.Utils;
using UnityEngine;

namespace Greg.Handlers
{
    public sealed partial class SpeedrunTimeUpdater
    {
        [EventHandler]
        private static void Handle(
            UpdateEvent _,
            IsGameEndedHolder isGameEndedHolder,
            StartRealTimeHolder startRealTimeHolder,
            ComponentsResource componentsResource
        )
        {
            if (isGameEndedHolder.IsGameEnded)
            {
                return;
            }
            
            // todo: start time at the real scene start
            // todo: show timer in the end screen
            var timer = startRealTimeHolder.GetTimer();
            
            foreach (var gameObject in componentsResource.Get<SpeedrunTimeComponent>())
            {
                gameObject.GetComponent<SpeedrunTimeComponent>().text.text = timer;
            }
        }
    }
}