using AreYouFruits.Math.Unity;
using UnityEngine;

namespace Greg.Utils
{
    public static class GeometryUtils
    {
        /// <summary>
        /// Rotates the Vector2 around the Z axis.
        /// </summary>
        /// <param name="v">Vector2 to rotate</param>
        /// <param name="angle">Angle in radians</param>
        public static Vector2 Rotate(Vector2 v, float angle)
        {
            var (x, y) = v;
            
            var sin = Mathf.Sin(angle);
            var cos = Mathf.Cos(angle);

            return new Vector2
            {
                x = cos * x - sin * y,
                y = sin * x + cos * y,
            };
        }

        public static Vector2 MulComponents(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2
            {
                x = lhs.x * rhs.x,
                y = lhs.y * rhs.y,
            };
        }

        public static Vector2 DivComponents(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2
            {
                x = lhs.x / rhs.x,
                y = lhs.y / rhs.y,
            };
        }

        public static Vector2Int MulComponents(Vector2Int lhs, Vector2Int rhs)
        {
            return new Vector2Int
            {
                x = lhs.x * rhs.x,
                y = lhs.y * rhs.y,
            };
        }

        public static Vector2Int DivComponents(Vector2Int lhs, Vector2Int rhs)
        {
            return new Vector2Int
            {
                x = lhs.x / rhs.x,
                y = lhs.y / rhs.y,
            };
        }

        public static Vector3 MulComponents(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3
            {
                x = lhs.x * rhs.x,
                y = lhs.y * rhs.y,
                z = lhs.z * rhs.z,
            };
        }

        public static Vector3 DivComponents(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3
            {
                x = lhs.x / rhs.x,
                y = lhs.y / rhs.y,
                z = lhs.z / rhs.z,
            };
        }

        public static Vector3Int MulComponents(Vector3Int lhs, Vector3Int rhs)
        {
            return new Vector3Int
            {
                x = lhs.x * rhs.x,
                y = lhs.y * rhs.y,
                z = lhs.z * rhs.z,
            };
        }

        public static Vector3Int DivComponents(Vector3Int lhs, Vector3Int rhs)
        {
            return new Vector3Int
            {
                x = lhs.x / rhs.x,
                y = lhs.y / rhs.y,
                z = lhs.z / rhs.z,
            };
        }

        public static Vector4 MulComponents(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4
            {
                x = lhs.x * rhs.x,
                y = lhs.y * rhs.y,
                z = lhs.z * rhs.z,
                w = lhs.w * rhs.w,
            };
        }

        public static Vector4 DivComponents(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4
            {
                x = lhs.x / rhs.x,
                y = lhs.y / rhs.y,
                z = lhs.z / rhs.z,
                w = lhs.w / rhs.w,
            };
        }

        public static Vector3 InverseLerp(Vector3 a, Vector3 b, Vector3 value)
        {
            return new Vector3
            {
                x = Mathf.InverseLerp(a.x, b.x, value.x),
                y = Mathf.InverseLerp(a.y, b.y, value.y),
                z = Mathf.InverseLerp(a.z, b.z, value.z),
            };
        }

        public static Vector3Int Clamp(Vector3Int value, Vector3Int min, Vector3Int max)
        {
            return new Vector3Int
            {
                x = Mathf.Clamp(value.x, min.x, max.x),
                y = Mathf.Clamp(value.y, min.y, max.y),
                z = Mathf.Clamp(value.z, min.z, max.z),
            };
        }
    }
}
