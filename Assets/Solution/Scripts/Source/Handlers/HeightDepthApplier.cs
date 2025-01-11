using AreYouFruits.Events;
using Greg.Components;
using Greg.Events;
using Greg.Global.Holders;
using Greg.Holders;
using UnityEngine;

namespace Greg.Handlers
{
    public sealed partial class HeightDepthApplier
    {
        [EventHandler]
        private static void Handle(
            UpdateEvent _,
            ComponentsResource componentsResource,
            BuiltDataHolder builtDataHolder
        )
        {
            foreach (var gameObject in componentsResource.Get<HeightDepthComponent>())
            {
                var heightDepth = gameObject.GetComponent<HeightDepthComponent>();

                var position = gameObject.transform.position;

                position.z = position.y * builtDataHolder.HeightDepthMultiplier + heightDepth.Offset;

                gameObject.transform.position = position;
            }
        }
    }
}