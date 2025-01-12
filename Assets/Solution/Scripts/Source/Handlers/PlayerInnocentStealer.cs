using System.Linq;
using AreYouFruits.Events;
using AreYouFruits.Nullability;
using Greg.Components;
using Greg.Data;
using Greg.Events;
using Greg.Global.Holders;
using Greg.Holders;
using UnityEngine;

namespace Greg.Handlers
{
    public sealed partial class PlayerInnocentStealer
    {
        [EventHandler]
        private static void Handle(
            PlayerStealInputEvent _,
            PlayerInteractionTargetHolder playerInteractionTargetHolder,
            InventoryItemsHolder inventoryItemsHolder,
            PlayerObjectHolder playerObjectHolder
        )
        {
            if (!playerInteractionTargetHolder.Value.IsInitialized)
            {
                return;
            }

            var interactionTargetComponent = playerInteractionTargetHolder.Value.GetOrThrow();

            if (interactionTargetComponent.InteractionTargetType != InteractionTargetType.Innocent)
            {
                return;
            }

            var pocketComponent = interactionTargetComponent.GetComponent<PocketComponent>();
            var playerHatComponent = playerObjectHolder.GameObject.GetComponent<HatComponent>();

            var hat = playerHatComponent.Hat.GetOrThrow();
            
            if (!pocketComponent.StoredItemId.IsInitialized || IsHatFull(hat))
            {
                return;
            }
            
            var stealableItemId = pocketComponent.StoredItemId.GetOrThrow();
            AddItemToAvailableSlot(hat, stealableItemId);
            pocketComponent.StoredItemId = Optional.None();
            
            inventoryItemsHolder.Add(stealableItemId);
        }

        private static bool IsHatFull(Hat hat)
        {
            return hat.InventorySlots.All(hatInventorySlot => hatInventorySlot.StoredItemId.IsInitialized);
        }
        
        private static void AddItemToAvailableSlot(Hat hat, uint itemId)
        {
            foreach (var hatInventorySlot in hat.InventorySlots)
            {
                if (hatInventorySlot.StoredItemId.IsInitialized)
                {
                    continue;
                }

                hatInventorySlot.StoredItemId.SetIfNull(itemId);
                break;
            }
        }
    }
}