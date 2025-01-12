using System;
using System.Collections.Generic;
using System.Linq;
using AreYouFruits.Events;
using Greg.Data;
using Greg.Events;
using Greg.Global.Holders;
using Greg.Holders;
using UnityEngine;

namespace Greg.Handlers
{
    public sealed partial class PathFindingInitializer
    {
        [EventHandler]
        private static void Handle(
            StartEvent _,
            BuiltDataHolder builtDataHolder,
            PathFinderHolder pathFinderHolder
        )
        {
            var cachedRooms = builtDataHolder.Rooms.ToDictionary(r => r.RoomId, r => new CachedPathFinderRoom
            {
                RoomId = r.RoomId,
                Area = r.Area,
                Transitions = r.Transitions.ToDictionary(t => t.RoomId),
            });

            var directionField = new Dictionary<(int, int), int>();

            foreach (var startRoomId in cachedRooms.Keys)
            {
                foreach (var finishRoomId in cachedRooms.Keys)
                {
                    var visitedRooms = new HashSet<int>();
                    var unvisitedRooms = cachedRooms.Keys.ToHashSet();

                    (float Length, int Room)? min = null;
                    
                    foreach (var transition in cachedRooms[startRoomId].Transitions.Values)
                    {
                        var distance = Vector2.Distance(cachedRooms[startRoomId].Area.center, transition.Door)
                                       + Vector2.Distance(cachedRooms[transition.RoomId].Area.center, transition.Door);
                        
                        if (GetLength(
                                cachedRooms,
                                visitedRooms,
                                unvisitedRooms,
                                transition.RoomId,
                                distance,
                                finishRoomId) is { } transitionLength)
                        {
                            if (min is not var (length, _) || transitionLength < length)
                            {
                                min = (transitionLength, transition.RoomId);
                            }
                        }
                    }

                    if (min is not var (_, minRoom))
                    {
                        throw new Exception();
                    }

                    directionField[(startRoomId, finishRoomId)] = minRoom;
                }
            }

            pathFinderHolder.DirectionField = directionField;
            pathFinderHolder.CachedRooms = cachedRooms;
        }
        
        private static float? GetLength(
            Dictionary<int, CachedPathFinderRoom> cachedRooms,
            HashSet<int> visited,
            HashSet<int> unvisited,
            int room,
            float previousDistance,
            int finish
        )
        {
            if (visited.Contains(room))
            {
                return null;
            }

            if (room == finish)
            {
                return previousDistance;
            }
            
            (float Length, int Room)? min = null;
                    
            foreach (var transition in cachedRooms[room].Transitions.Values)
            {
                var distance = previousDistance
                               + Vector2.Distance(cachedRooms[room].Area.center, transition.Door)
                               + Vector2.Distance(cachedRooms[transition.RoomId].Area.center, transition.Door);

                visited.Add(room);
                unvisited.Remove(room);
                
                if (GetLength(
                        cachedRooms,
                        visited,
                        unvisited,
                        transition.RoomId,
                        distance,
                        finish) is { } transitionLength)
                {
                    if (min is not var (length, _) || transitionLength < length)
                    {
                        min = (transitionLength, transition.RoomId);
                    }
                }

                visited.Remove(room);
                unvisited.Add(room);
            }

            if (min is var (minLength, _))
            {
                return minLength;
            }

            return null;
        }
    }
}