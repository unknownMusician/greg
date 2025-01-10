using Greg.Utils.TagSearcher;
using UnityEngine;

namespace Greg.Utils
{
    [ScriptTag(ArchitectureTag.Component)]
    public sealed class GizmoSphere : MonoBehaviour
    {
        [field: SerializeField] public Color Color { get; set; }
        [field: SerializeField] public bool IsWire { get; set; }
        [field: SerializeField] public float Radius { get; set; }

        private void OnDrawGizmos()
        {
            var lastColor = Gizmos.color;
            Gizmos.color = Color;

            if (IsWire)
            {
                Gizmos.DrawWireSphere(transform.position, Radius);
            }
            else
            {
                Gizmos.DrawSphere(transform.position, Radius);
            }

            Gizmos.color = lastColor;
        }
    }
}
