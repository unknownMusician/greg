using AreYouFruits.Nullability;
using UnityEngine;

namespace Greg.Components
{
    public sealed class WalkingNpcComponent : MonoBehaviour
    {
        [HideInInspector]
        public Optional<int> TargetIndex;
        public float WaitedTime;
        public float NeededTime;
    }
}