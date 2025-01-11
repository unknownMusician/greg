using AreYouFruits.Events;
using Greg.Components;
using Greg.Events;
using Greg.Global.Holders;
using Greg.Holders;

namespace Greg.Handlers
{
    public sealed partial class GuardsLookAreaVisualizer
    {
        [EventHandler]
        private static void Handle(
            UpdateEvent _,
            BuiltDataHolder builtDataHolder,
            ComponentsResource componentsResource
        )
        {
            foreach (var guard in componentsResource.Get<GuardComponent>())
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