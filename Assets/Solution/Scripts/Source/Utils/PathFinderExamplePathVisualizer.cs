using AreYouFruits.Events;
using Greg.Holders;
using UnityEngine;

namespace Greg.Utils
{
    public sealed class PathFinderExamplePathVisualizer : MonoBehaviour
    {
        [SerializeField] private Transform start;
        [SerializeField] private Transform end;

        private void OnDrawGizmos()
        {
            var pathFinderHolder = ResourcesLocator.Get<PathFinderHolder>();
            var target = PathFinderUtils.GetTarget(pathFinderHolder, start.position, end.position);
            
            Gizmos.color = Color.red;
            Gizmos.DrawLine(start.position, target);
        }
    }
}