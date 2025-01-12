using AreYouFruits.VectorsSwizzling;
using UnityEngine;

namespace Greg.Utils
{
    public class Cell
    {
        public Vector3 Position { get; set; }
        public bool CanWalk { get; set; }
    }
    
    public static class PathFindingGridCreator
    {
        public static Cell[,] Create(Vector3 min, Vector3 max, float gridSize)
        {
            var rowCount = Mathf.RoundToInt((max.y - min.y) / gridSize);
            var columnCount = Mathf.RoundToInt((max.x - min.x) / gridSize);
            
            var grid = new Cell[rowCount, columnCount];
            
            for (var i = 0; i < columnCount; i++)
            {
                for (var j = 0; j < rowCount; j++)
                {
                    var position = new Vector3(min.x + i * gridSize, min.y + j * gridSize);
                    var canWalk = !Physics2D.OverlapBox(position.XY(), new Vector2(gridSize, gridSize), 0, 6);
                    grid[j, i] = new Cell
                    {
                        Position = position,
                        CanWalk = canWalk,
                    };
                }
            }

            var unwalkableCells = 0;
            for (var i = 0; i < columnCount; i++)
            {
                for (var j = 0; j < rowCount; j++)
                {
                    if (!grid[j, i].CanWalk)
                    {
                        unwalkableCells++;
                    }
                }
            }
            Debug.Log(unwalkableCells);

            return grid;
        }
    }
}