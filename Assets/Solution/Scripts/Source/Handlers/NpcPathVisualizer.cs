using AreYouFruits.Events;
using AreYouFruits.VectorsSwizzling;
using Greg.Components;
using Greg.Events;
using UnityEngine;

namespace Greg.Handlers
{
    public sealed partial class NpcPathVisualizer
    {
        [EventHandler]
        private static void Handle(
            OnDrawGizmosEvent _
        )
        {
#if UNITY_EDITOR

            if (!UnityEditor.Selection.activeGameObject.TryGetComponent(out WalkingNpcComponent walkingNpcComponent))
            {
                return;
            }

            var path = walkingNpcComponent.WalkPath;

            Gizmos.color = Color.magenta;
            
            for (var i = 0; i < path.Count; i++)
            {
                var nextIndex = (i + 1) % path.Count;
                
                Gizmos.DrawSphere(path[i].Position.XYN(), 0.5f);
                Gizmos.DrawLine(path[i].Position.XYN(), path[nextIndex].Position.XYN());
            }
#endif
        }
    }
}