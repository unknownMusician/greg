using AreYouFruits.Events;
using UnityEngine;

namespace Greg.Events
{
    public struct PlayerMoveInputEvent : IEvent
    {
        public Vector3 Direction { get; set; }
    }
}