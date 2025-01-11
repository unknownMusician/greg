using AreYouFruits.Events;
using Greg.Components;
using Greg.Events;
using Greg.Global.Api;
using Greg.Global.Holders;
using Greg.Holders;
using UnityEngine;

namespace Greg.Handlers
{
    public sealed partial class GuardsLook
    {
        [EventHandler]
        private static void Handle(
            UpdateEvent _,
            PlayerObjectHolder playerObjectHolder,
            BuiltDataHolder builtDataHolder,
            ComponentsResource componentsResource
            )
        {
            foreach (var guard in componentsResource.Get<GuardComponent>())
            {
                var distanceVector = playerObjectHolder.GameObject.transform.position - guard.transform.position;
                if (distanceVector.magnitude > builtDataHolder.GuardLookDistance)
                {
                    continue;
                }

                var angle = Vector3.Angle(distanceVector, guard.GetComponent<GuardLookDirectionComponent>().Direction);
                if (angle > builtDataHolder.GuardLookAngle / 2)
                {
                    continue;
                }

                var hit = Physics2D.Raycast(guard.transform.position, distanceVector, float.MaxValue, 6);
                if (hit.collider != null)
                {
                    return;
                }
                
                EventContext.Bus.Invoke(new GuardDetectedPlayerEvent
                {
                    Guard = guard,
                });
            }
        }
    }
}