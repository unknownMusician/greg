using AreYouFruits.Events;
using Greg.Components;
using Greg.Events;
using Greg.Global.Api;
using Greg.Global.Holders;
using Greg.Holders;
using UnityEngine;

namespace Greg.Handlers
{
    public sealed partial class PlayerStealer
    {
        [EventHandler]
        private static void Handle(
            PlayerStealInputEvent _,
            StealablesHolder stealablesHolder,
            PlayerObjectHolder playerObjectHolder,
            BuiltDataHolder builtDataHolder,
            InventoryItemsHolder inventoryItemsHolder
            )
        {
            Debug.Log($"[PlayerStealer]");

            var shortestDistance = Mathf.Infinity;
            GameObject closestStealable = null;
            foreach (var stealable in stealablesHolder.Stealables)
            {
                var distance = (stealable.transform.position - playerObjectHolder.GameObject.transform.position).magnitude;

                if (distance <= builtDataHolder.StealDistance && distance < shortestDistance)
                {
                    closestStealable = stealable;
                }
            }

            EventContext.Bus.Invoke(new PlayerStealEvent());

            if (closestStealable == null)
            {
                return;
            }

            inventoryItemsHolder.Add(closestStealable.GetComponent<StealableIdComponent>().Id);
            
            stealablesHolder.Stealables.Remove(closestStealable);
            Object.Destroy(closestStealable);
        }
    }
}