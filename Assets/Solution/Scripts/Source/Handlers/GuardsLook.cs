using AreYouFruits.Events;
using AreYouFruits.Nullability;
using AreYouFruits.VectorsSwizzling;
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
            if (!IsIllegal())
            {
                return;
            }

            foreach (var guard in componentsResource.Get<GuardComponent>())
            {
                var distanceVector = playerObjectHolder.GameObject.transform.position.XY() - guard.transform.position.XY();
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

                var hatId = playerObjectHolder.GameObject.GetComponent<HatComponent>().Hat.GetOrThrow().HatId;
                guard.GetComponent<GuardInvestigateGoalComponent>().GoalHatId = new Optional<uint>(hatId);
                
                EventContext.Bus.Invoke(new GuardDetectedPlayerEvent
                {
                    Guard = guard,
                });
            }
        }

        private static bool IsIllegal()
        {
            return Input.GetKey(KeyCode.F) || !Input.GetKey(KeyCode.E);
        }
    }
}