using AreYouFruits.Events;
using Greg.Utils.TagSearcher;
using UnityEngine;

namespace Greg.Global
{
    [ScriptTag(ArchitectureTag.Global)]
    public sealed class HandlersRegisterer : MonoBehaviour, IHandlerRegisterer
    {
        public void Register(EventBus eventBus)
        {
            eventBus.Subscribe(new InitializeResources());
        }
    }
}
