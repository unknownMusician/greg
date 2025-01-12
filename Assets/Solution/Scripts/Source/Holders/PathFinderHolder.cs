using System.Collections.Generic;
using Greg.Data;

namespace Greg.Holders
{
    public sealed class PathFinderHolder
    {
        public Dictionary<int, CachedPathFinderRoom> CachedRooms;
        public Dictionary<(int, int), int> DirectionField;
    }
}