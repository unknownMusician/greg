using AreYouFruits.Events;
using Greg.Events;
using Greg.Global.Holders;
using Solution.Scripts.Source.Components;
using Solution.Scripts.Source.Holders;

namespace Solution.Scripts.Source.Handlers
{
    public sealed partial class GuardsLookAreaVisualizer
    {
        [EventHandler]
        private static void Handle(
            UpdateEvent _,
            GuardsHolder guardsHolder,
            BuiltDataHolder builtDataHolder
            )
        {
            foreach (var guard in guardsHolder.Guards)
            {
                var direction = guard.GetComponent<GuardLookDirectionComponent>().Direction;
                var lookArea = guard.GetComponent<GuardLookAreaComponent>().LookArea;

                lookArea.transform.up = direction;
                lookArea.pointLightOuterAngle = builtDataHolder.GuardLookAngle;
                lookArea.pointLightOuterRadius = builtDataHolder.GuardLookDistance;
            }
        }
    }
}