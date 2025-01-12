using System.Collections.Generic;
using AreYouFruits.Events;
using Greg.Events;
using Greg.Global.Holders;
using Greg.Utils;
using UnityEngine;

namespace Greg.Handlers
{
    public sealed partial class PathFindingGridInitializer
    {
        [EventHandler]
        private static void Handle(
            StartEvent _,
            SceneDataHolder sceneDataHolder,
            BuiltDataHolder builtDataHolder
            )
        { 
            PathFindingGridCreator.Create(sceneDataHolder.LevelBounds.min, sceneDataHolder.LevelBounds.max, builtDataHolder.PathfindingGridSize);
        }
    }
    
    public struct Node
    {
        public Vector3 Position { get; set; }
        public Vector3 Previous { get; set; }
        public int Cost { get; set; }
        
        public Node(Vector3 position)
        {
            Position = position;
            Previous = new Vector3(-1, -1);
            Cost = int.MaxValue;
        }

        public bool HasPosition()
        {
            return Position != new Vector3(-1, -1);
        }
        
        public bool HasPrevious()
        {
            return Previous != new Vector3(-1, -1);
        }
    }
    
    public static class PathFinder
    {
        public static Vector3[] GetShortestPath(Cell[,] cells, Vector3 from, Vector3 goal)
        {
            var reachable = new Dictionary<Vector3, Node>()
            {
                { from, new Node(from) }
            };
            var explored = new Dictionary<Vector3, Node>();

            while (reachable.Count > 0)
            {
                var node = ChooseNode(reachable, goal);

                if (!node.HasPosition())
                {
                    break;
                }

                if (node.Position == goal)
                {
                    var path = new List<Vector3>();
                    while (node.HasPosition() && node.HasPrevious())
                    {
                        path.Add(node.Position);
                        node = explored[node.Previous];
                    }

                    path.Reverse();
                    return path.ToArray();
                }

                reachable.Remove(node.Position);
                explored.Add(node.Position, node);

                var neighbours = GetMoves(cells, node.Position);
                for (var i = 0; i < neighbours.Count; i++)
                {
                    if (reachable.ContainsKey(neighbours[i]) || explored.ContainsKey(neighbours[i]))
                    {
                        continue;
                    }

                    var neighbourNode = new Node(neighbours[i]);

                    if (node.Cost + 1 < neighbourNode.Cost)
                    {
                        neighbourNode.Previous = node.Position;
                        neighbourNode.Cost = node.Cost + 1;
                    }

                    reachable.Add(neighbours[i], neighbourNode);
                }
            }

            return new Vector3[] { };
        }

        private static List<Vector3> GetMoves(Cell[,] cells, Vector3 origin)
        {
            var moves = new List<Vector3>();

            var coordinates = Vector2Int.zero;
            for (var i = 0; i < cells.GetLength(0); i++)
            {
                for (var j = 0; j < cells.GetLength(1); j++)
                {
                    if (cells[i, j].Position == origin)
                    {
                        coordinates = new Vector2Int(i, j);
                    }
                }
            }
            
            if (coordinates.x - 1 >= 0 && cells[coordinates.x - 1, coordinates.y].CanWalk)
            {
                moves.Add(cells[coordinates.x - 1, coordinates.y].Position);
            }
            
            if (coordinates.x + 1 < cells.GetLength(0) && cells[coordinates.x + 1, coordinates.y].CanWalk)
            {
                moves.Add(cells[coordinates.x + 1, coordinates.y].Position);
            }
            
            if (coordinates.y - 1 < cells.GetLength(1) && cells[coordinates.x, coordinates.y - 1].CanWalk)
            {
                moves.Add(cells[coordinates.x, coordinates.y - 1].Position);
            }
            
            if (coordinates.y + 1 < cells.GetLength(1) && cells[coordinates.x, coordinates.y + 1].CanWalk)
            {
                moves.Add(cells[coordinates.x, coordinates.y + 1].Position);
            }

            return moves;
        }

        public static bool PathExists(Cell[,] cells, Vector3 from, Vector3 goal)
        {
            return GetShortestPath(cells, from, goal).Length > 0;
        }

        public static int GetShortestPathLength(Cell[,] cells, Vector3 from, Vector3 goal)
        {
            return GetShortestPath(cells, from, goal).Length;
        }

        private static Node ChooseNode(Dictionary<Vector3, Node> reachable, Vector3 goal)
        {
            var minCost = int.MaxValue;
            Node bestNode = new Node(new Vector3(-1, -1));

            foreach (var (position, node) in reachable)
            {
                var costStartToNode = node.Cost;
                var costNodeToGoal = Mathf.RoundToInt((goal - position).magnitude);
                var totalCost = costStartToNode + costNodeToGoal;

                if (minCost > totalCost)
                {
                    minCost = totalCost;
                    bestNode = node;
                }
            }

            return bestNode;
        }
    }

}