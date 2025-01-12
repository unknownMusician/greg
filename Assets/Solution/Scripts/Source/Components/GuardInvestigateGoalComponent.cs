using AreYouFruits.Nullability;
using UnityEngine;

namespace Greg.Components
{
    public sealed class GuardInvestigateGoalComponent : MonoBehaviour
    {
        public Optional<uint> GoalHatId { get; set; }
    }
}