using AreYouFruits.Events;
using Greg.Events;
using Greg.Global.Holders;
using UnityEngine;

namespace Greg.Handlers
{
    public sealed partial class PathFindingVisualizer
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
        
        [EventHandler]
        private static void Handle(
            OnDrawGizmosEvent _,
            BuiltDataHolder builtDataHolder
        )
        {
            {
                foreach (var room in builtDataHolder.Rooms)
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
        }

        private static Color ColorByRoomId(int roomId)
        {
            return colors[roomId % colors.Length];
        }
    }
}