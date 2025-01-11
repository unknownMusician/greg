using AreYouFruits.Nullability;
using Greg.Data;
using UnityEngine;

namespace Greg.Components
{
    public sealed class HatComponent : MonoBehaviour
    {
        public Optional<Hat> Hat { get; set; }
    }
}