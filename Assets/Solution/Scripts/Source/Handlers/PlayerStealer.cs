using AreYouFruits.Events;
using Greg.Events;
using Greg.Global.Holders;
using Solution.Scripts.Source.Components;
using Solution.Scripts.Source.Holders;
using UnityEngine;

namespace Solution.Scripts.Source.Handlers
{
    public sealed partial class PlayerStealer
    {
        [EventHandler]
        private static void Handle(
            PlayerStealInputEvent _,
            StealablesHolder stealablesHolder,
            SceneDataHolder sceneDataHolder,
            BuiltDataHolder builtDataHolder,
            InventoryItemsHolder inventoryItemsHolder
            )
        {
            var shortestDistance = Mathf.Infinity;
            GameObject closestStealable = null;
            foreach (var stealable in stealablesHolder.Stealables)
            {
                var distance = (stealable.transform.position - sceneDataHolder.Player.transform.position).magnitude;

                if (distance <= builtDataHolder.StealDistance && distance < shortestDistance)
                {
                    closestStealable = stealable;
                }
            }

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