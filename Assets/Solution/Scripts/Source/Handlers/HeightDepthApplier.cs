using AreYouFruits.Events;
using Greg.Components;
using Greg.Events;
using Greg.Holders;
using UnityEngine;

namespace Greg.Handlers
{
    public sealed partial class HeightDepthApplier
    {
        [EventHandler]
        private static void Handle(
            StartEvent _,
            ComponentsResource componentsResource
        )
        {
            foreach (var gameObject in componentsResource.Get<HeightDepthComponent>())
            {
                var heightDepth = gameObject.GetComponent<HeightDepthComponent>();
                
                heightDepth.
            }
        }
    }
}