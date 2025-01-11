using AreYouFruits.Events;
using Greg.Events;
using Solution.Scripts.Source.Components;
using Solution.Scripts.Source.Holders;
using UnityEngine;

namespace Solution.Scripts.Source.Handlers
{
    public sealed partial class GuardsLookDirectionUpdater
    {
        [EventHandler]
        private static void Handle(
            UpdateEvent _,
            GuardsHolder guardsHolder
        )
        {
            foreach (var guard in guardsHolder.Guards)
            {
                // var direction = guard.GetComponent<Rigidbody2D>().linearVelocity.normalized;
                var direction = Vector3.left;
                guard.GetComponent<GuardLookDirectionComponent>().Direction = direction;
            }
        }
    }
}