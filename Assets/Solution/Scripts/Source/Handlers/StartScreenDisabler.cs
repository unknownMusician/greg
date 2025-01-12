using AreYouFruits.Events;
using Greg.Events;
using Greg.Global.Holders;
using Greg.Holders;
using UnityEngine;

namespace Greg.Handlers
{
    public sealed partial class StartScreenDisabler
    {
        [EventHandler]
        private static void Handle(
            UpdateEvent _,
            SceneDataHolder sceneDataHolder,
            StartRealTimeHolder timeHolder
        )
        {
            if (Input.anyKeyDown && sceneDataHolder.StartScreen.activeSelf)
            {
                timeHolder.Time = 0;
                sceneDataHolder.StartScreen.SetActive(false);
            }
        }
    }
}