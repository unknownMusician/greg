using AreYouFruits.Events;
using Greg.Components;
using Greg.Data;
using Greg.Events;
using Greg.Global.Holders;
using Greg.Holders;

namespace Greg.Handlers
{
    public sealed partial class GuardsPlayerLostHandler
    {
        [EventHandler]
        private static void Handle(
            UpdateEvent _,
            ComponentsResource componentsResource,
            PlayerObjectHolder playerObjectHolder,
            BuiltDataHolder builtDataHolder
            )
        {
            foreach (var guard in componentsResource.Get<GuardComponent>())
            {
                var guardStateComponent = guard.GetComponent<GuardStateComponent>();
                if (guardStateComponent.State != GuardStateType.Aggressive)
                {
                    continue;
                }
                
                var distance = (playerObjectHolder.GameObject.transform.position - guard.transform.position).magnitude;
                if (distance > builtDataHolder.GuardLookDistance)
                {
                    guardStateComponent.State = GuardStateType.Investigative;
                }
            }
        }
    }
}