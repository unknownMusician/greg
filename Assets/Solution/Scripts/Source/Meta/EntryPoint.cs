using System;
using AreYouFruits.Events;
using Greg.Events;
using Greg.Global;
using Greg.Global.Api;
using Greg.Utils.TagSearcher;
using UnityEngine;

namespace Greg.Meta
{
    [ScriptTag(ArchitectureTag.Global)]
    [DefaultExecutionOrder(-9999)]
    public sealed class EntryPoint : MonoBehaviour
    {
        private void Awake()
        {
            var globalOrderer = new GroupGraphOrderer();

            foreach (var orderer in gameObject.GetComponentsInChildren<IHandlerOrderer>())
            {
                orderer.Order(globalOrderer);
            }
            
            var graphOrderer = globalOrderer.ToGraphOrderer();

            var determinismInfoHolder = new HandlersOrderingDeterminismInfoHolder(graphOrderer.Relations);

            ResourcesLocator.Debugger = determinismInfoHolder;

            var eventBus = new EventBus(graphOrderer.ToCached(0), determinismInfoHolder);

            EventContext.Bus = eventBus;

            RegisterMeta(eventBus);
            
            foreach (var registerer in gameObject.GetComponentsInChildren<IHandlerRegisterer>())
            {
                registerer.Register(eventBus);
            }
        }

        private static void RegisterMeta(EventBus eventBus)
        {
            // eventBus.Subscribe<IEvent>(EventLogger.Log);
        }

        private void Start()
        {
            EventContext.Bus.Invoke(new StartEvent());
        }

        private void Update()
        {
            EventContext.Bus.Invoke(new EarlyUpdateEvent());
            EventContext.Bus.Invoke(new UpdateEvent());
        }

        private void LateUpdate()
        {
            EventContext.Bus.Invoke(new LateUpdateEvent());
        }

        private void OnDrawGizmos()
        {
            EventContext.Bus?.Invoke(new OnDrawGizmosEvent());
        }

        private void OnDestroy()
        {
            EventContext.Bus.Invoke(new OnDestroyEvent());
            
            EventContext.Bus = null;
            ResourcesLocator.Debugger = null;
        }
    }
}
