using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Greg.Utils
{
    public sealed class ByDistanceVector2Comparer : IComparer<Vector2Int>
    {
        public Vector2Int target;
        public Vector2Int start;
        public Vector2 min;
        public Vector2 cellScale;
        
        public int Compare(Vector2Int x, Vector2Int y)
        {
            return GetHeuristic(y).CompareTo(GetHeuristic(x));
        }

        private float GetHeuristic(Vector2Int index)
        {
            var distanceToTarget = (IndexToPosition(index) - IndexToPosition(target)).magnitude;
            var distanceToStart = (IndexToPosition(index) - IndexToPosition(start)).magnitude;
            
            return distanceToTarget + distanceToStart;
        }

        private Vector2 IndexToPosition(Vector2Int index)
        {
            return Pathfinder.IndexToPosition(index, min, cellScale);
        }
    }
    
    public sealed class Pathfinder : MonoBehaviour
    {
        private static IReadOnlyList<(Vector2Int, float)> LocalNeighbors = new (Vector2Int, float)[]
        {
            (new(0, 1), 1),
            (new(1, 1), 1.414f),
            (new(1, 0), 1),
            (new(1, -1), 1.414f),
            (new(0, -1), 1),
            (new(-1, -1), 1.414f),
            (new(-1, 0), 1),
            (new(-1, 1), 1.414f),
        };
        
        [SerializeField] private Vector2 min;
        [SerializeField] private Vector2 max;
        [SerializeField] private Vector2 cellScale;
        [SerializeField] private LayerMask obstacleLayerMask;
        [Space]
        [SerializeField] private Transform start;
        [SerializeField] private Transform end;

        private Vector2Int cellCount;
        private bool[,] fieldIsObstacle;

        private void Awake()
        {
            min = Vector2.Min(min, max);
            max = Vector2.Max(min, max);
            
            var scale = max - min;
            
            cellCount = new Vector2Int
            {
                x = Mathf.CeilToInt(scale.x / cellScale.x),
                y = Mathf.CeilToInt(scale.y / cellScale.y),
            };
        }
        
        private void Start()
        {
            fieldIsObstacle = new bool[cellCount.x, cellCount.y];

            for (var y = 0; y < cellCount.y; y++)
            {
                for (var x = 0; x < cellCount.x; x++)
                {
                    var cellCenter = IndexToPosition(new Vector2Int(x, y));

                    var isObstacle = Physics2D.OverlapBox(cellCenter, cellScale, 0, obstacleLayerMask) != null;

                    fieldIsObstacle[x, y] = isObstacle;
                }
            }
        }

        private void OnDrawGizmos()
        {
            for (var y = 0; y < cellCount.y; y++)
            {
                for (var x = 0; x < cellCount.x; x++)
                {
                    var cellCenter = IndexToPosition(new Vector2Int(x, y));

                    var color = fieldIsObstacle[x, y] switch
                    {
                        true => Color.red,
                        false => Color.green,
                    };

                    Gizmos.color = color;
                    Gizmos.DrawWireCube(cellCenter, cellScale);
                }
            }
            
            var path = GetPath(start.position, end.position);
            
            Gizmos.DrawLine(start.position, path[0]);
            
            Gizmos.color = Color.cyan;

            for (var i = 0; i < path.Count - 1; i++)
            {
                Gizmos.DrawLine(path[i], path[i + 1]);
            }
        }

        public List<Vector2> GetPath(Vector2 start, Vector2 finish)
        {
            var startIndex = PositionToIndex(start);
            var finishIndex = PositionToIndex(finish);

            var fieldMinDistanceFromStart = new Dictionary<Vector2Int, float>();
            var fieldClosestStartCell = new Dictionary<Vector2Int, Vector2Int>();

            var comparer = new ByDistanceVector2Comparer
            {
                target = finishIndex,
                start = startIndex,
                cellScale = cellScale,
                min = min,
            };

            // todo: Check and probably invert comparer.
            var remembered = new List<Vector2Int>();
            
            remembered.Add(startIndex);
            fieldMinDistanceFromStart.Add(startIndex, 0);
            fieldClosestStartCell.Add(startIndex, startIndex);

            while (remembered.Any())
            {
                remembered.Sort(comparer);
                var closest = remembered[^1];
                remembered.RemoveAt(remembered.Count - 1);
                
                foreach (var (localNeighbor, localDistance) in LocalNeighbors)
                {
                    var neighborIndex = closest + localNeighbor;

                    if (!CanMoveAtIndex(neighborIndex))
                    {
                        continue;
                    }

                    var neighborDistanceToStart = fieldMinDistanceFromStart[closest] + localDistance;

                    if (!fieldMinDistanceFromStart.ContainsKey(neighborIndex))
                    {
                        remembered.Add(neighborIndex);
                    }
                    
                    if (!fieldMinDistanceFromStart.TryGetValue(neighborIndex, out var minDistance)
                        || neighborDistanceToStart < minDistance)
                    {
                        fieldMinDistanceFromStart[neighborIndex] = neighborDistanceToStart;
                        fieldClosestStartCell[neighborIndex] = closest;
                    }

                    if (neighborIndex == finishIndex)
                    {
                        var path = new List<Vector2>();

                        path.Add(finish);

                        var cellIndex = closest;
                        
                        while (cellIndex != startIndex)
                        {
                            path.Add(IndexToPosition(cellIndex));
                            cellIndex = fieldClosestStartCell[cellIndex];
                        }

                        path.Reverse();

                        return path;
                    }
                }
            }
            
            return new List<Vector2>();
        }

        private Vector2 IndexToPosition(Vector2Int index)
        {
            return min + cellScale / 2 + index * cellScale;
        }

        public static Vector2 IndexToPosition(Vector2Int index, Vector2 min, Vector2 cellScale)
        {
            return min + cellScale / 2 + index * cellScale;
        }

        private bool CanMoveAtIndex(Vector2Int index)
        {
            return ContainsIndex(index) && !fieldIsObstacle[index.x, index.y];
        }

        private bool ContainsIndex(Vector2Int index)
        {
            var result = true;

            result &= index.x >= 0;
            result &= index.x < cellCount.x;
            result &= index.y >= 0;
            result &= index.y < cellCount.y;

            return result;
        }

        private Vector2Int PositionToIndex(Vector2 position)
        {
            var progress = (position - min).Divide(max - min);

            var index = Vector2Int.FloorToInt(progress * cellCount);

            return new Vector2Int
            {
                x = Mathf.Clamp(index.x, 0, cellCount.x - 1),
                y = Mathf.Clamp(index.y, 0, cellCount.y - 1),
            };
        }
    }
}