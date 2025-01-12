using System;
using UnityEngine;

namespace Greg.Data
{
    [Serializable]
    public struct PathFinderRoom
    {
        public int RoomId;
        public Rect Area; 
        public PathFinderRoomTransition[] Transitions;
    }
}