using AreYouFruits.Nullability;
using UnityEngine;

namespace Greg.Components
{
    public sealed class GuardInvestigateGoalComponent : MonoBehaviour
    {
        public Optional<uint> GoalItemId { get; set; }
    }
}