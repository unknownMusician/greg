using AreYouFruits.Events;
using Greg.Components;
using Greg.Events;
using Greg.Holders;
using UnityEngine;

namespace Greg.Handlers
{
    public sealed partial class GuardLookAreaVisualizer
    {
        [EventHandler]
        private static void Handle(
            UpdateEvent _,
            ComponentsResource componentsResource
            )
        {
            foreach (var guard in componentsResource.Get<GuardComponent>())
            {
                var direction = guard.GetComponent<GuardLookDirectionComponent>().Direction;
                var lookAreaTransform = guard.GetComponent<GuardLookAreaComponent>().LookArea.transform;
                lookAreaTransform.up = direction;
                lookAreaTransform.rotation = Quaternion.Euler(
                    lookAreaTransform.rotation.eulerAngles.x, 
                    lookAreaTransform.rotation.eulerAngles.y, 
                    lookAreaTransform.rotation.eulerAngles.z - 180);
            }
        }
    }
}