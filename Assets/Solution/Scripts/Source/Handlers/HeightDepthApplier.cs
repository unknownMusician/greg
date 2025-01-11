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
            UpdateEvent _,
            ComponentsResource componentsResource
        )
        {
            foreach (var gameObject in componentsResource.Get<HeightDepthComponent>())
            {
                var heightDepth = gameObject.GetComponent<HeightDepthComponent>();

                var position = gameObject.transform.position;

                position.z = position.y * heightDepth.Multiplier + heightDepth.Offset;

                gameObject.transform.position = position;
            }
        }
    }
}