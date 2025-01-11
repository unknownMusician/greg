using AreYouFruits.Events;
using Greg.Data;
using UnityEngine;

namespace Greg.Events
{
    public struct CharacterSpawnedEvent : IEvent
    {
        public GameObject GameObject;
        public CharacterType CharacterType;
    }
}