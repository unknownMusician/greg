using AreYouFruits.Events;
using Greg.Components;
using Greg.Events;
using Greg.Holders;
using UnityEngine;

namespace Greg.Handlers
{
    public sealed partial class GuardsLookDirectionUpdater
    {
        [EventHandler]
        private static void Handle(
            UpdateEvent _,
            ComponentsResource componentsResource
        )
        {
            foreach (var guard in componentsResource.Get<GuardComponent>())
            {
                // var direction = guard.GetComponent<Rigidbody2D>().linearVelocity.normalized;
                var direction = Vector3.left;
                guard.GetComponent<GuardLookDirectionComponent>().Direction = direction;
            }
        }
    }
}