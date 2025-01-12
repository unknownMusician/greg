using AreYouFruits.Events;
using Greg.Holders;
using UnityEngine;

namespace Greg.Utils
{
    public sealed class ComponentRegisterer : MonoBehaviour
    {
        private void Start()
        {
            ResourcesLocator.Get<ComponentsResource>().Register(gameObject);
        }

        private void OnDestroy()
        {
            ResourcesLocator.TryGet<ComponentsResource>().Switch(r => r.Unregister(gameObject));
        }
    }
}