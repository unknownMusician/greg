using System;
using UnityEngine;

namespace Greg.Data
{
    [Serializable]
    public struct PathFinderRoomTransition
    {
        public int RoomId;
        public Vector2 Door;
    }
}