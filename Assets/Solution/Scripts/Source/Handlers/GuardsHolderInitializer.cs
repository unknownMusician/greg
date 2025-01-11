using AreYouFruits.Events;
using Greg.Events;
using Greg.Global.Holders;
using Solution.Scripts.Source.Components;
using Solution.Scripts.Source.Holders;

namespace Solution.Scripts.Source.Handlers
{
    public sealed partial class GuardsHolderInitializer
    {
        [EventHandler]
        private static void Handle(
            StartEvent _,
            SceneDataHolder sceneDataHolder,
            GuardsHolder guardsHolder
        )
        {
            foreach (var guard in sceneDataHolder.GuardsContainer.GetComponentsInChildren<GuardComponent>())
            {
                guardsHolder.Guards.Add(guard.gameObject);
            }
        }
    }
}