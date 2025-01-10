using AreYouFruits.Events;
using Greg.Events;
using Greg.Utils.TagSearcher;
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
        }

        private static void OrderUpdate(GroupGraphOrderer orderer)
        {
            var eventOrderer = orderer.ForEvent<UpdateEvent>();
        }
    }
}
