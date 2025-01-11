using AreYouFruits.Events;
using Greg.Events;
using Greg.Global.Api;
using Greg.Global.Holders;
using Solution.Scripts.Source.Components;
using Solution.Scripts.Source.Holders;
using UnityEngine;

namespace Solution.Scripts.Source.Handlers
{
    public sealed partial class GuardsLook
    {
        [EventHandler]
        private static void Handle(
            UpdateEvent _,
            GuardsHolder guardsHolder,
            SceneDataHolder sceneDataHolder,
            BuiltDataHolder builtDataHolder
            )
        {
            foreach (var guard in guardsHolder.Guards)
            {
                var distanceVector = sceneDataHolder.Player.transform.position - guard.transform.position;
                if (distanceVector.magnitude > builtDataHolder.GuardLookDistance)
                {
                    continue;
                }

                var angle = Vector3.Angle(distanceVector, guard.GetComponent<GuardLookDirectionComponent>().Direction);
                if (angle > builtDataHolder.GuardLookAngle)
                {
                    continue;
                }

                var hit = Physics2D.Raycast(guard.transform.position, distanceVector);
                if (hit.collider != null && hit.collider.gameObject.GetComponentInParent<PlayerComponent>())
                {
                    EventContext.Bus.Invoke(new GuardDetectedPlayerEvent
                    {
                        Guard = guard,
                    });
                }
            }
        }
    }
}