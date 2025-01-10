using AreYouFruits.Events;
using Greg.Events;
using Greg.Global.Api;
using UnityEngine;

namespace Solution.Scripts.Source.Handlers
{
    public sealed partial class PlayerMoveInputReader
    {
        [EventHandler]
        private static void Handle(
            UpdateEvent _
        )
        {
            var direction = Vector3.zero;

            if (Input.GetKey(KeyCode.W))
            {
                direction += Vector3.up;
            }
            
            if (Input.GetKey(KeyCode.A))
            {
                direction += Vector3.left;
            }
            
            if (Input.GetKey(KeyCode.S))
            {
                direction += Vector3.down;
            }
            
            if (Input.GetKey(KeyCode.D))
            {
                direction += Vector3.right;
            }

            if (direction != Vector3.zero)
            {
                direction.Normalize();
            }
            
            EventContext.Bus.Invoke(new PlayerMoveInputEvent
            {
                Direction = direction,
            });
        }
    }
}