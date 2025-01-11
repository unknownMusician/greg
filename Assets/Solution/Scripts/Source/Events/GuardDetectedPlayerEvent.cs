using AreYouFruits.Events;
using UnityEngine;

namespace Greg.Events
{
    public struct GuardDetectedPlayerEvent : IEvent
    {
        public GameObject Guard { get; set; }
    }
}