using AreYouFruits.Events;
using Greg.Components;
using Greg.Events;
using Greg.Holders;
using Greg.Utils;
using UnityEngine;

namespace Greg.Handlers
{
    public sealed partial class SpeedrunTimeResetter
    {
        [EventHandler]
        private static void Handle(
            StartEvent _,
            StartRealTimeHolder startRealTimeHolder
        )
        {
            startRealTimeHolder.Time = 0;
        }
    }
    
    public sealed partial class SpeedrunTimeUpdater
    {
        [EventHandler]
        private static void Handle(
            UpdateEvent _,
            StartRealTimeHolder startRealTimeHolder
        )
        {
            startRealTimeHolder.Time += Time.deltaTime;
        }
    }
    
    public sealed partial class SpeedrunTimeViewUpdater
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
            
            var timer = startRealTimeHolder.GetTimer();
            
            foreach (var gameObject in componentsResource.Get<SpeedrunTimeComponent>())
            {
                gameObject.GetComponent<SpeedrunTimeComponent>().text.text = timer;
            }
        }
    }
}