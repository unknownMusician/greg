using System;
using System.Collections.Generic;
using System.Linq;
using Greg.Global.Api;
using AreYouFruits.Collections;
using AreYouFruits.Events;

namespace Greg.Meta
{
    public partial class HandlersOrderingDeterminismInfoHolder
    {
        private static readonly Dictionary<Type, HashSet<Type>> ResourceAccessesByHandler = new();
        private static readonly Dictionary<Type, HashSet<Type>> HandlersByResourceAccess = new();
        
        private static readonly Dictionary<Type, HashSet<Type>> ParentsByHandler = new();
        private static readonly Dictionary<Type, HashSet<Type>> ChildrenByHandler = new();
        
        private static readonly Dictionary<Type, Type> StoredEventByHandler = new();
        
        private static readonly Dictionary<Type, Type> StoredResourceByAccess = new();
        private static readonly Dictionary<Type, HashSet<Type>> StoredAccessesByResource = new();
        
        private static readonly HashSet<(Type Previous, Type Next)> StoredOrderings = new();
        
        private static readonly HashSet<Type> StoredReadOnlyResourceAccessAccesses = new();

        public static int Snapshot { get; private set; }

        public static IReadOnlyCollection<Type> Handlers => ResourceAccessesByHandler.Keys;
        public static IReadOnlyCollection<Type> ResourceAccesses => HandlersByResourceAccess.Keys;
        public static IReadOnlyCollection<Type> Resources => StoredAccessesByResource.Keys;
        
        public static IReadOnlyDictionary<Type, Type> EventByHandler => StoredEventByHandler;
        
        public static IReadOnlyDictionary<Type, Type> ResourceByAccess => StoredResourceByAccess;
        
        public static IReadOnlyCollection<(Type Previous, Type Next)> Orderings => StoredOrderings;
        
        public static IReadOnlyCollection<Type> ReadOnlyResourceAccesses => StoredReadOnlyResourceAccessAccesses;

        public static IReadOnlyCollection<Type> GetResourcesByHandler(Type handlerType)
        {
            if (ResourceAccessesByHandler.TryGetValue(handlerType, out var resources))
            {
                return resources;
            }
            
            return Array.Empty<Type>();
        }

        public static IReadOnlyCollection<Type> GetAccessesByResource(Type resourceType)
        {
            if (StoredAccessesByResource.TryGetValue(resourceType, out var resourcesAccesses))
            {
                return resourcesAccesses;
            }
            
            return Array.Empty<Type>();
        }

        public static IReadOnlyCollection<Type> GetHandlersByResourceAccess(Type resourceAccessType)
        {
            if (HandlersByResourceAccess.TryGetValue(resourceAccessType, out var handlers))
            {
                return handlers;
            }
            
            return Array.Empty<Type>();
        }

        public static IReadOnlyCollection<Type> GetHandlerParents(Type handlerType)
        {
            if (ParentsByHandler.TryGetValue(handlerType, out var parents))
            {
                return parents;
            }
            
            return Array.Empty<Type>();
        }

        public static IReadOnlyCollection<Type> GetHandlerChildren(Type handlerType)
        {
            if (ChildrenByHandler.TryGetValue(handlerType, out var children))
            {
                return children;
            }
            
            return Array.Empty<Type>();
        }
    }
    
    public sealed partial class HandlersOrderingDeterminismInfoHolder : IHandlersDebugger, IResourcesDebugger
    {
        private readonly List<Type> activeHandlers = new();
        
        public HandlersOrderingDeterminismInfoHolder(IEnumerable<(Type, Type)> orderings)
        {
            ResourceAccessesByHandler.Clear();
            HandlersByResourceAccess.Clear();
            StoredOrderings.Clear();
            ParentsByHandler.Clear();
            ChildrenByHandler.Clear();

            foreach (var ordering in orderings)
            {
                StoredOrderings.Add(ordering);
            }

            Snapshot++;
        }
        
        public void HandleHandlerStarting(Type handlerType)
        {
            var isChanged = false;
            
            if (!StoredEventByHandler.ContainsKey(handlerType))
            {
                StoredEventByHandler.Add(handlerType, GetHandlerEventType(handlerType));
                isChanged = true;
            }
            
            foreach (var activeHandler in activeHandlers)
            {
                isChanged |= ParentsByHandler.GetOrInsertNew(handlerType).Add(activeHandler);
                isChanged |= ChildrenByHandler.GetOrInsertNew(activeHandler).Add(handlerType);
            }

            activeHandlers.Add(handlerType);

            if (isChanged)
            {
                Snapshot++;
            }
        }

        private static Type GetHandlerEventType(Type handlerType)
        {
            foreach (var @interface in handlerType.GetInterfaces())
            {
                if (!@interface.IsGenericType)
                {
                    continue;
                }
                
                if (@interface.GetGenericTypeDefinition() == typeof(IEventHandler<>))
                {
                    return @interface.GetGenericArguments()[0];
                }
            }

            throw new ArgumentOutOfRangeException();
        }

        public void HandleHandlerEnded(Type handlerType)
        {
            activeHandlers.RemoveAt(activeHandlers.Count - 1);
        }

        public void HandleResourceAccessed(Type resourceType, Type resourceAccessType, bool isReadonly)
        {
            var isChanged = false;
            
            if (isReadonly)
            {
                isChanged |= StoredReadOnlyResourceAccessAccesses.Add(resourceAccessType);
            }

            isChanged |= StoredResourceByAccess.TryAdd(resourceAccessType, resourceType);
            isChanged |= StoredAccessesByResource.GetOrInsertNew(resourceType).Add(resourceAccessType);

            if (activeHandlers.Any())
            {
                var activeHandler = activeHandlers.Last();
                
                isChanged |= ResourceAccessesByHandler.GetOrInsertNew(activeHandler).Add(resourceAccessType);
                isChanged |= HandlersByResourceAccess.GetOrInsertNew(resourceAccessType).Add(activeHandler);
            }

            if (isChanged)
            {
                Snapshot++;
            }
        }
    }
}
