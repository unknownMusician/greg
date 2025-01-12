using UnityEngine;

namespace Greg.Utils
{
    public static class VectorExtensions
    {
        public static Vector2 Divide(this Vector2 lhs, Vector2 rhs)
        {
            return new Vector2
            {
                x = lhs.x / rhs.x,
                y = lhs.y / rhs.y,
            };
        }
    }
}