using AreYouFruits.Events;
using Greg.Events;
using Greg.Utils.TagSearcher;
using UnityEngine;

namespace Greg.Global.Handlers
{
    [ScriptTag(ArchitectureTag.Global)]
    public sealed class InitializePredefinedResources : MonoHandlerRegisterer<StartEvent>
    {
        [SerializeField] private Object[] _resources;
        
        public override void Handle(StartEvent @event)
        {
            foreach (var resource in _resources)
            {
                ResourcesLocator.Add(resource);
            }
        }
    }
}