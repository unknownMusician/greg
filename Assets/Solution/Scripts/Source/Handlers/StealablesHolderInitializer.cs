using AreYouFruits.Events;
using Greg.Components;
using Greg.Events;
using Greg.Global.Holders;
using Greg.Holders;

namespace Greg.Handlers
{
    public sealed partial class StealablesHolderInitializer
    {
        [EventHandler]
        private static void Handle(
            StartEvent _,
            SceneDataHolder sceneDataHolder,
            StealablesHolder stealablesHolder
        )
        {
            foreach (var stealable in sceneDataHolder.EnvironmentContainer.GetComponentsInChildren<StealableComponent>())
            {
                stealablesHolder.Stealables.Add(stealable.gameObject);
            }
        }
    }
}