using AreYouFruits.Events;
using Greg.Components;
using Greg.Events;
using Greg.Holders;
using UnityEngine;

namespace Greg.Handlers
{
    public sealed partial class SpeedrunTimeUpdater
    {
        [EventHandler]
        private static void Handle(
            UpdateEvent _,
            StartRealTimeHolder startRealTimeHolder,
            ComponentsResource componentsResource
        )
        {
            // todo: start time at the real scene start
            // todo: show timer in the end screen
            
            var time = Time.realtimeSinceStartup - startRealTimeHolder.StartRealTime;

            var roundedTime = Mathf.FloorToInt(time);

            var minutes = roundedTime / 60;
            var seconds = roundedTime % 60;
            var milliSeconds = Mathf.FloorToInt(time * 1000) % 1000;
            
            var formattedTime = $"{minutes:00}:{seconds:00}.{milliSeconds:000}";
            
            foreach (var gameObject in componentsResource.Get<SpeedrunTimeComponent>())
            {
                gameObject.GetComponent<SpeedrunTimeComponent>().text.text = formattedTime;
            }
        }
    }
}