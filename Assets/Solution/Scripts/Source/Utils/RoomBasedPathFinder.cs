using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AreYouFruits.Events;
using Greg.Data;
using NUnit.Framework;
using UnityEngine;

namespace Greg.Utils
{
    public sealed class RoomBasedPathFinder : MonoBehaviour
    {
        private static readonly Color[] colors = new Color[]
        {
            Color.green,
            new Color(0.0f, 0.4f, 0.4f),
            new Color(0.8f, 0.3f, 0.5f),
            Color.blue,
            Color.cyan,
            Color.magenta,
            new Color(0.5f, 0.7f, 0.4f),
            Color.red,
            new Color(0.5f, 0.7f, 0.8f),
            Color.yellow,
            new Color(0.5f, 0.4f, 0.8f),
        };
        
        [SerializeField] private PathFinderRoom[] rooms;
        
        [SerializeField] private Transform start;
        [SerializeField] private Transform end;

        private Dictionary<int, CachedPathFinderRoom> cachedRooms;

        private Dictionary<(int, int), int> directionField;
        
        private void Start()
        {
            cachedRooms = rooms.ToDictionary(r => r.RoomId, r => new CachedPathFinderRoom
            {
                RoomId = r.RoomId,
                Area = r.Area,
                Transitions = r.Transitions.ToDictionary(t => t.RoomId),
            });

            directionField = new Dictionary<(int, int), int>();

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
                        
                        if (GetLength(visitedRooms, unvisitedRooms, transition.RoomId, distance, finishRoomId) is { } transitionLength)
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
            
            Debug.Log("Hui");
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(start.position, GetDirection(start.position, end.position));
        }

        private Vector2 GetDirection(Vector2 start, Vector2 target)
        {
            var startRoomId = (int?)null;
            var finishRoomId = (int?)null;

            var closestStartRoomId = ((int room, float distance)?)null;
            var closestFinishRoomId = ((int room, float distance)?)null;
            
            foreach (var room in cachedRooms.Values)
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

            var targetRoom = directionField[(startRoom, finishRoom)];

            return cachedRooms[startRoom].Transitions[targetRoom].Door;
        }

        private float? GetLength(HashSet<int> visited, HashSet<int> unvisited, int room, float previousDistance, int finish)
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
                
                if (GetLength(visited, unvisited, transition.RoomId, distance, finish) is { } transitionLength)
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

        private void OnDrawGizmosSelected()
        {
            foreach (var room in rooms)
            {
                Gizmos.color = ColorByRoomId(room.RoomId);
                Gizmos.DrawWireCube(room.Area.center, room.Area.size);
                
                foreach (var transition in room.Transitions)
                {
                    Gizmos.color = ColorByRoomId(transition.RoomId);
                    Gizmos.DrawSphere(transition.Door, 15);
                }
            }
        }

        private static Color ColorByRoomId(int roomId)
        {
            return colors[roomId % colors.Length];
        }
    }
}