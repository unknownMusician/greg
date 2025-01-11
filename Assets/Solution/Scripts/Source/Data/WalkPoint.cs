using System;
using UnityEngine;

namespace Greg.Data
{
    [Serializable]
    public struct WalkPoint
    {
        public Vector2 Position;
        public float WaitDuration;
    }
}