using System.Collections.Generic;
using Greg.Data;
using UnityEngine;

namespace Greg.Components
{
    public sealed class WalkingNpcComponent : MonoBehaviour
    {
        public List<WalkPoint> WalkPath;
        public int TargetIndex;
        public float WaitedTime;
    }
}