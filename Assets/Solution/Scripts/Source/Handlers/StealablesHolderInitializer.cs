using AreYouFruits.Events;
using Greg.Events;
using Greg.Global.Holders;
using Solution.Scripts.Source.Components;
using Solution.Scripts.Source.Holders;

namespace Solution.Scripts.Source.Handlers
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