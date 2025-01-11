using AreYouFruits.Events;
using Greg.Events;
using Greg.Global.Handlers;
using Greg.Utils.TagSearcher;
using Solution.Scripts.Source.Handlers;
using UnityEngine;

namespace Greg.Global
{
    [ScriptTag(ArchitectureTag.Global)]
    public sealed class HandlersOrderer : MonoBehaviour, IHandlerOrderer
    {
        public void Order(GroupGraphOrderer orderer)
        {
            OrderStart(orderer);
            OrderUpdate(orderer);
        }

        private static void OrderStart(GroupGraphOrderer orderer)
        {
            var eventOrderer = orderer.ForEvent<StartEvent>();
            
            eventOrderer.Order<InitializePredefinedResources>().Before<StealablesHolderInitializer>();
            eventOrderer.Order<InitializePredefinedResources>().Before<GuardsHolderInitializer>();
            eventOrderer.Order<InitializePredefinedResources>().Before<InventoryViewCreator>();
        }

        private static void OrderUpdate(GroupGraphOrderer orderer)
        {
            var eventOrderer = orderer.ForEvent<UpdateEvent>();
            
            eventOrderer.Order<GuardsLookDirectionUpdater>().Before<GuardsLook>();
        }
    }
}
