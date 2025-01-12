using System;
using System.Collections.Generic;
using System.Reflection;
using AreYouFruits.Events;
using AreYouFruits.Nullability;
using Greg.Events;
using Greg.Global.Api;
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
            typeof(ResourcesLocator)
                .GetField("Resourses", BindingFlags.Static | BindingFlags.NonPublic)
                ?.SetValue(null, new TypedDictionary());

            EventContext.Bus = new EventBus(new CachedOrderProvider(new Optional<IReadOnlyDictionary<Type, int>>(),
                    new Optional<int>()));

            SceneManager.LoadScene(0);
        }
    }
}