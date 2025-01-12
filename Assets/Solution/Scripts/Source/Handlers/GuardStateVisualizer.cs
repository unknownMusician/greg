using System.Linq;
using AreYouFruits.Events;
using Greg.Components;
using Greg.Data;
using Greg.Events;
using Greg.Global.Holders;
using Greg.Holders;

namespace Greg.Handlers
{
    public sealed partial class GuardStateVisualizer
    {
        [EventHandler]
        private static void Handle(
            UpdateEvent _,
            ComponentsResource componentsResource,
            BuiltDataHolder builtDataHolder
            )
        {
            foreach (var guard in componentsResource.Get<GuardComponent>())
            {
                var state = guard.GetComponent<GuardStateComponent>().State;
                var guardStateViewComponent = guard.GetComponent<GuardStateViewComponent>();
                guardStateViewComponent.StateContainer.SetActive(state != GuardStateType.Calm);
                guardStateViewComponent.ExclamationMark.SetActive(state == GuardStateType.Aggressive);
                guardStateViewComponent.Icon.gameObject.SetActive(state == GuardStateType.Investigative);

                if (state == GuardStateType.Investigative && 
                    guard.GetComponent<GuardInvestigateGoalComponent>().GoalHatId.TryGet(out var goalItemId))
                {
                    guardStateViewComponent.Icon.sprite = builtDataHolder.HatSettings.First(s => s.Id == goalItemId).Icon;
                }
            }
        }
    }
}