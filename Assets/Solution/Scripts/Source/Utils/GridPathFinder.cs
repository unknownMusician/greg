using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AreYouFruits.VectorsSwizzling;
using UnityEngine;

namespace Greg.Utils
{
    public struct AaGrid2
    {
        public Vector2 Min;
        public Vector2 Max;
        public float CellSize;
        
        public readonly Vector2 GridScale => Max - Min;
        public readonly Vector2Int CellsCount => Vector2Int.CeilToInt(GridScale / CellSize);
        
        public readonly Vector2Int PositionToIndex(Vector2 position)
        {
            var index = PositionToIndexUnclamped(position);

            index.Clamp(Vector2Int.zero, CellsCount - Vector2Int.one);

            return index;
        }
        
        public readonly Vector2Int PositionToIndexUnclamped(Vector2 position)
        {
            var progress = (position - Min).Divide(Max - Min);

            return Vector2Int.FloorToInt(progress * CellsCount);
        }

        public readonly Vector2 IndexToPosition(Vector2Int index)
        {
            return Min + CellSize * (Vector2.one / 2 + index);
        }

        public readonly bool ContainsIndex(Vector2Int index)
        {
            var result = true;

            result &= index.x >= 0 && index.x < CellsCount.x;
            result &= index.y >= 0 && index.y < CellsCount.y;

            return result;
        }
    }

    public struct NeighborCellInfo
    {
        public Vector2Int LocalPosition;
        public float LocalDistance;

        public NeighborCellInfo(Vector2Int localPosition, float localDistance)
        {
            LocalPosition = localPosition;
            LocalDistance = localDistance;
        }
    }

    public struct KnownPathFinderCellInfo
    {
        public Vector2Int NextCell;
        public float MinDistance;

        public KnownPathFinderCellInfo(Vector2Int nextCell, float minDistance)
        {
            NextCell = nextCell;
            MinDistance = minDistance;
        }
    }
    
    public sealed class GridPathFinder : MonoBehaviour
    {
        private static IReadOnlyList<NeighborCellInfo> LocalNeighbors = new NeighborCellInfo[]
        {
            new(new(0, 1), 1),
            new(new(1, 1), 1.414f),
            new(new(1, 0), 1),
            new(new(1, -1), 1.414f),
            new(new(0, -1), 1),
            new(new(-1, -1), 1.414f),
            new(new(-1, 0), 1),
            new(new(-1, 1), 1.414f),
        };
        
        [SerializeField] private Vector2 min;
        [SerializeField] private Vector2 max;
        // todo: To vector2.
        [SerializeField] private float cellSize;
        [SerializeField] private LayerMask obstacleLayerMask;
        [SerializeField] private Transform start;
        [SerializeField] private Transform finish;

        private AaGrid2 grid;
        private bool[,] fieldIsObstacle;

        [ContextMenu("GenerateGrid")]
        private void GenerateGrid()
        {
            grid = new AaGrid2
            {
                CellSize = cellSize,
                Min = min,
                Max = max,
            };

            var cellsCount = grid.CellsCount;

            fieldIsObstacle = new bool[cellsCount.x, cellsCount.y];

            for (var y = 0; y < cellsCount.y; y++)
            {
                for (var x = 0; x < cellsCount.x; x++)
                {
                    var position = grid.IndexToPosition(new Vector2Int(x, y));

                    var collider = Physics2D.OverlapBox(
                        position,
                        Vector2.one * cellSize,
                        0,
                        obstacleLayerMask
                    );

                    fieldIsObstacle[x, y] = collider != null;
                }
            }
        }
        
        private void Start()
        {
            GenerateGrid();
        }

        public List<Vector2> GetPath(Vector2 start, Vector2 finish)
        {
            var startIndex = grid.PositionToIndex(start);
            var finishIndex = grid.PositionToIndex(finish);

            if (startIndex == finishIndex)
            {
                return new List<Vector2> { finish };
            }

            var visited = new List<Vector2Int> { finishIndex };
            var knownCells = new Dictionary<Vector2Int, KnownPathFinderCellInfo>();
            
            knownCells.Add(finishIndex, new KnownPathFinderCellInfo(default, 0));

            var byDistanceVector2Comparer = new ByDistanceVector2Comparer
            {
                target = startIndex,
                grid = grid,
                knownCells = knownCells,
            };

            while (visited.Any())
            {
                visited.Sort(byDistanceVector2Comparer);
                var closingIndex = visited[^1];
                visited.RemoveAt(visited.Count - 1);

                if (closingIndex == startIndex)
                {
                    var path = new List<Vector2>();
                    
                    var cellIndex = closingIndex;
                    
                    if (startIndex != grid.PositionToIndexUnclamped(start))
                    {
                        path.Add(grid.IndexToPosition(startIndex));
                    }
                        
                    while (cellIndex != finishIndex)
                    {
                        if (cellIndex != startIndex)
                        {
                            path.Add(grid.IndexToPosition(cellIndex));
                        }

                        var newCellIndex = knownCells[cellIndex].NextCell;

                        if (newCellIndex == cellIndex && cellIndex != finishIndex)
                        {
                            throw new Exception();
                        }
                        
                        cellIndex = newCellIndex;
                    }
                    
                    if (finishIndex != grid.PositionToIndexUnclamped(finish))
                    {
                        path.Add(grid.IndexToPosition(finishIndex));
                    }

                    path.Add(finish);

                    return path;
                }
                
                foreach (var localNeighbor in LocalNeighbors)
                {
                    var neighborIndex = closingIndex + localNeighbor.LocalPosition;
                    var neighborPotentialLength = knownCells[closingIndex].MinDistance + localNeighbor.LocalDistance;

                    if (!IsWalkableIndex(neighborIndex))
                    {
                        continue;
                    }

                    if (!knownCells.ContainsKey(neighborIndex))
                    {
                        visited.Add(neighborIndex);
                    }

                    if (knownCells.TryGetValue(neighborIndex, out var knownNeighbor))
                    {
                        if (neighborPotentialLength < knownNeighbor.MinDistance)
                        {
                            knownCells[neighborIndex] =
                                new KnownPathFinderCellInfo(closingIndex, neighborPotentialLength);
                        }
                    }
                    else
                    {
                        knownCells[neighborIndex] =
                            new KnownPathFinderCellInfo(closingIndex, neighborPotentialLength);
                    }
                }
            }

            return new List<Vector2>();
        }

        private bool IsWalkableIndex(Vector2Int index)
        {
            return grid.ContainsIndex(index) && !fieldIsObstacle[index.x, index.y];
        }

        private void OnDrawGizmos()
        {
            var cellsCount = grid.CellsCount;

            for (var y = 0; y < cellsCount.y; y++)
            {
                for (var x = 0; x < cellsCount.x; x++)
                {
                    Gizmos.color = fieldIsObstacle[x, y] switch
                    {
                        true => Color.red,
                        false => Color.green,
                    };
                    
                    Gizmos.DrawWireCube(grid.IndexToPosition(new(x, y)), grid.CellSize * Vector2.one);
                }
            }
            
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(start.position, 1);
            
            Gizmos.color = Color.cyan * new Color(0.3f, 0.3f, 0.3f, 1.0f);
            Gizmos.DrawSphere(finish.position, 1);

            var path = GetPath(start.position, finish.position);

            var pathStartColor = Color.white;
            var pathFinishColor = Color.blue;

            path.Insert(0, start.position);
            
            for (var i = 0; i < path.Count - 1; i++)
            {
                Gizmos.color = Color.Lerp(pathStartColor, pathFinishColor, (float)i / (path.Count - 1));
                Gizmos.DrawLine(path[i], path[i + 1]);
            }
        }
    }
}