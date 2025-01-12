using System;
using System.Collections.Generic;
using UnityEngine;

namespace Greg.Data
{
    [Serializable]
    public struct CachedPathFinderRoom
    {
        public int RoomId;
        public Rect Area; 
        public Dictionary<int, PathFinderRoomTransition> Transitions;
    }
}