using System;
using Greg.Holders;
using UnityEngine;

namespace Greg.Utils
{
    public static class PathFinderUtils
    {
        public static Vector2 GetDirection(PathFinderHolder pathFinderHolder, Vector2 start, Vector2 target)
        {
            var startRoomId = (int?)null;
            var finishRoomId = (int?)null;

            var closestStartRoomId = ((int room, float distance)?)null;
            var closestFinishRoomId = ((int room, float distance)?)null;
            
            foreach (var room in pathFinderHolder.CachedRooms.Values)
            {
                if (room.Area.Contains(start))
                {
                    startRoomId = room.RoomId;
                }
                if (room.Area.Contains(target))
                {
                    finishRoomId = room.RoomId;
                }

                if (startRoomId is not null && finishRoomId is not null)
                {
                    break;
                }

                var startLength = Vector2.Distance(room.Area.center, start); 
                var finishLength = Vector2.Distance(room.Area.center, target);

                if (closestStartRoomId is not var (_, startDistance) || startLength < startDistance)
                {
                    closestStartRoomId = (room.RoomId, startLength);
                }

                if (closestFinishRoomId is not var (_, finishDistance) || finishLength < finishDistance)
                {
                    closestFinishRoomId = (room.RoomId, finishLength);
                }
            }

            if (startRoomId is null && closestStartRoomId is var (newClosestStart, _))
            {
                startRoomId = newClosestStart;
            }

            if (finishRoomId is null && closestFinishRoomId is var (newClosestFinish, _))
            {
                finishRoomId = newClosestFinish;
            }

            if (startRoomId is not { } startRoom)
            {
                throw new Exception();
            }
            
            if (finishRoomId is not { } finishRoom)
            {
                throw new Exception();
            }

            if (startRoom == finishRoom)
            {
                return target;
            }

            var targetRoom = pathFinderHolder.DirectionField[(startRoom, finishRoom)];

            return pathFinderHolder.CachedRooms[startRoom].Transitions[targetRoom].Door;
        }
    }
}