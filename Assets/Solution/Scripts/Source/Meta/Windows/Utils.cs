#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Linq;
using AreYouFruits.Collections;

namespace Greg.Meta.Windows
{
    public static class Utils
    {
        public static HashSet<(Type, Type)> GetRedundantOrderings(Dictionary<(Type Handler0, Type Handler1), HashSet<Type>> neededOrderings)
        {
            var redundantOrderings = new HashSet<(Type, Type)>();
            foreach (var (previous, next) in HandlersOrderingDeterminismInfoHolder.Orderings)
            {
                if (!neededOrderings.ContainsKey((previous, next))
                 && !neededOrderings.ContainsKey((next, previous)))
                {
                    redundantOrderings.Add((previous, next));
                }
            }

            return redundantOrderings;
        }

        public static Dictionary<(Type, Type), HashSet<Type>> GetMissingAbsoluteOrderings(
            Dictionary<(Type Handler0, Type Handler1), HashSet<Type>> neededOrderings
        )
        {
            var missingOrderings = new Dictionary<(Type, Type), HashSet<Type>>();

            foreach (var ((handler0, handler1), resources) in neededOrderings)
            {
                if (!HandlersOrderingDeterminismInfoHolder.Orderings.Contains((handler0, handler1))
                 && !HandlersOrderingDeterminismInfoHolder.Orderings.Contains((handler1, handler0)))
                {
                    missingOrderings.Add((handler0, handler1), resources);
                }
            }

            return missingOrderings;
        }

        public static Dictionary<(Type, Type), HashSet<Type>> GetMissingRelativeOrderings(
            Dictionary<(Type Handler0, Type Handler1), HashSet<Type>> neededOrderings
        )
        {
            var missingOrderings = new Dictionary<(Type, Type), HashSet<Type>>();

            foreach (var ((handler0, handler1), resources) in neededOrderings)
            {
                var nextFor0 = new HashSet<Type>();
                var previousFor0 = new HashSet<Type>();
                var nextFor1 = new HashSet<Type>();
                var previousFor1 = new HashSet<Type>();

                foreach (var (previous, next) in HandlersOrderingDeterminismInfoHolder.Orderings)
                {
                    if (previous == handler0)
                    {
                        nextFor0.Add(next);
                    }

                    if (previous == handler1)
                    {
                        nextFor1.Add(next);
                    }

                    if (next == handler0)
                    {
                        previousFor0.Add(previous);
                    }

                    if (next == handler1)
                    {
                        previousFor1.Add(previous);
                    }
                }

                var areOrdered = false;

                foreach (var next1 in nextFor1)
                {
                    if (previousFor0.Contains(next1))
                    {
                        areOrdered = true;
                        break;
                    }
                }

                if (areOrdered)
                {
                    continue;
                }
                
                foreach (var next0 in nextFor0)
                {
                    if (previousFor1.Contains(next0))
                    {
                        areOrdered = true;
                        break;
                    }
                }

                if (areOrdered)
                {
                    continue;
                }

                if (!HandlersOrderingDeterminismInfoHolder.Orderings.Contains((handler0, handler1))
                 && !HandlersOrderingDeterminismInfoHolder.Orderings.Contains((handler1, handler0)))
                {
                    missingOrderings.Add((handler0, handler1), resources);
                }
            }

            return missingOrderings;
        }

        public static Dictionary<(Type Handler0, Type Handler1), HashSet<Type>> GetNeededOrderings()
        {
            var neededOrderings = new Dictionary<(Type Handler0, Type Handler1), HashSet<Type>>();

            var readOnlyResourceAccesses = HandlersOrderingDeterminismInfoHolder.ReadOnlyResourceAccesses;
            
            foreach (var resource in HandlersOrderingDeterminismInfoHolder.Resources)
            {
                var accesses = HandlersOrderingDeterminismInfoHolder.GetAccessesByResource(resource);

                foreach (var access in accesses)
                {
                    var handlers = HandlersOrderingDeterminismInfoHolder.GetHandlersByResourceAccess(access);
                    
                    foreach (var otherAccess in accesses)
                    {
                        if (readOnlyResourceAccesses.Contains(access) && readOnlyResourceAccesses.Contains(otherAccess))
                        {
                            continue;
                        }
                        
                        var otherHandlers = HandlersOrderingDeterminismInfoHolder.GetHandlersByResourceAccess(otherAccess);

                        foreach (var handler in handlers)
                        {
                            var eventType = HandlersOrderingDeterminismInfoHolder.EventByHandler[handler];
                                
                            foreach (var otherHandler in otherHandlers)
                            {
                                if (handler == otherHandler)
                                {
                                    continue;
                                }

                                HashSet<(Type, Type)> handlersOrParentsWithSameEvent;

                                if (eventType != HandlersOrderingDeterminismInfoHolder.EventByHandler[otherHandler])
                                {
                                    handlersOrParentsWithSameEvent = GetHandlersOrParentsWithSameEvent(handler, otherHandler);
                                }
                                else
                                {
                                    handlersOrParentsWithSameEvent = new HashSet<(Type, Type)>()
                                    {
                                        (handler, otherHandler),
                                    };
                                }

                                foreach (var (handlerWithSameEvent, otherHandlerWithSameEvent) in
                                    handlersOrParentsWithSameEvent)
                                {
                                    if (handlerWithSameEvent == otherHandlerWithSameEvent)
                                    {
                                        continue;
                                    }
                                    
                                    if (!neededOrderings.ContainsKey((handlerWithSameEvent, otherHandlerWithSameEvent))
                                     && !neededOrderings.ContainsKey((otherHandlerWithSameEvent, handlerWithSameEvent)))
                                    {
                                        neededOrderings.GetOrInsertNew((handlerWithSameEvent, otherHandlerWithSameEvent)).Add(resource);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            
            return neededOrderings;
        }

        public static HashSet<(Type, Type)> GetHandlersOrParentsWithSameEvent(Type handler0, Type handler1)
        {
            var results = new HashSet<(Type, Type)>();

            var selfAndParentHandlers0 = GetSelfAndParentHandlers(handler0);
            var selfAndParentHandlers1 = GetSelfAndParentHandlers(handler1);

            foreach (var handlerOrParent0 in selfAndParentHandlers0)
            {
                foreach (var handlerOrParent1 in selfAndParentHandlers1)
                {
                    if (handlerOrParent0 == handlerOrParent1)
                    {
                        continue;
                    }

                    var event0 = HandlersOrderingDeterminismInfoHolder.EventByHandler[handlerOrParent0];
                    var event1 = HandlersOrderingDeterminismInfoHolder.EventByHandler[handlerOrParent1];
                    
                    if (event0 == event1)
                    {
                        if (!results.Contains((handlerOrParent0, handlerOrParent1))
                         && !results.Contains((handlerOrParent1, handlerOrParent0)))
                        {
                            results.Add((handlerOrParent0, handlerOrParent1));
                        }
                    }
                }
            }
            
            return results;
        }

        public static HashSet<Type> GetSelfAndParentHandlers(Type handler)
        {
            var results = new HashSet<Type>();
            var checkedHandlers = new HashSet<Type>();

            results.Add(handler);

            while (results.Count != checkedHandlers.Count)
            {
                var typesToCheck = new List<Type>();

                foreach (var result in results)
                {
                    if (checkedHandlers.Add(result))
                    {
                        typesToCheck.Add(result);
                    }
                }

                foreach (var typeToCheck in typesToCheck)
                {
                    foreach (var handlerParent in HandlersOrderingDeterminismInfoHolder.GetHandlerParents(typeToCheck))
                    {
                        results.Add(handlerParent);
                    }
                }
            }

            return results;
        }
    }
}
#endif