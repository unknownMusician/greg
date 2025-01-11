using System;
using System.Collections.Generic;
using AreYouFruits.Collections;
using UnityEngine;

namespace Greg.Holders
{
    public sealed class ComponentsResource
    {
        private readonly Dictionary<Type, HashSet<GameObject>> _componentsByType = new();
        
        public void Register(GameObject gameObject)
        {
            foreach (var component in gameObject.GetComponents<Component>())
            {
                _componentsByType.GetOrInsertNew(component.GetType()).Add(gameObject);
            }
        }

        public void Unregister(GameObject gameObject)
        {
            foreach (var component in gameObject.GetComponents<Component>())
            {
                _componentsByType[component.GetType()].Remove(gameObject);
            }
        }
        
        public IReadOnlyCollection<GameObject> Get<TComponent>()
            where TComponent : Component
        {
            if (!_componentsByType.TryGetValue(typeof(TComponent), out var components))
            {
                return Array.Empty<GameObject>();
            }

            return components;
        }
    }
}