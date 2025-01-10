using UnityEngine;

namespace Greg.Utils.Extensions
{
    public static class BoundsExtensions
    {
        public static Vector3 GetRandomPointInsideBounds(this Bounds bounds)
        {
            return new Vector3
            {
                x = Random.Range(bounds.min.x, bounds.max.x),
                y = Random.Range(bounds.min.y, bounds.max.y),
                z = Random.Range(bounds.min.z, bounds.max.z),
            };
        }
    }
}