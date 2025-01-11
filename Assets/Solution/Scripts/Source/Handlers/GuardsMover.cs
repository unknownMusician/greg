using AreYouFruits.Events;
using Greg.Events;
using Solution.Scripts.Source.Holders;
using UnityEngine.AI;

namespace Solution.Scripts.Source.Handlers
{
    public sealed partial class GuardsMover
    {
        [EventHandler]
        private static void Handle(
            UpdateEvent _,
            GuardsHolder guardsHolder
            )
        {
            foreach (var guard in guardsHolder.Guards)
            {
                
            }
        }
    }
}