using AreYouFruits.Events;
using Greg.Components;
using Greg.Data;
using Greg.Events;
using Greg.Global.Api;
using Greg.Global.Holders;
using Greg.Holders;

namespace Greg.Handlers
{
    public sealed partial class GuardsPlayerCatcher
    {
        [EventHandler]
        private static void Handle(
            UpdateEvent _,
            ComponentsResource componentsResource,
            PlayerObjectHolder playerObjectHolder,
            BuiltDataHolder builtDataHolder,
            IsGameEndedHolder isGameEndedHolder
            )
        {
            foreach (var guard in componentsResource.Get<GuardComponent>())
            {
                var guardStateComponent = guard.GetComponent<GuardStateComponent>();

                if (guardStateComponent.State == GuardStateType.Calm)
                {
                    continue;
                }

                if (guardStateComponent.State == GuardStateType.Aggressive)
                {
                    var distance = (guard.transform.position - playerObjectHolder.GameObject.transform.position).magnitude;
                    if (distance <= builtDataHolder.CatchDistance)
                    {
                        isGameEndedHolder.IsGameEnded = true;
                        EventContext.Bus.Invoke(new GameEndedEvent());
                    }
                }
            }
        }
    }
}