using System.Linq;
using AreYouFruits.Events;
using Greg.Components;
using Greg.Data;
using Greg.Events;
using Greg.Global.Api;
using Greg.Global.Holders;
using Greg.Holders;
using UnityEngine;

namespace Greg.Handlers
{
    public sealed partial class InnocentHatSwapper
    {
        [EventHandler]
        private static void Handle(
            HatSwapInputEvent _,
            PlayerInteractionTargetHolder playerInteractionTargetHolder,
            PlayerObjectHolder playerObjectHolder,
            BuiltDataHolder builtDataHolder,
            SceneDataHolder sceneDataHolder,
            InventoryItemsHolder inventoryItemsHolder
        )
        {
            if (!playerInteractionTargetHolder.Value.IsInitialized)
            {
                return;
            }
            
            var interactionTargetComponent = playerInteractionTargetHolder.Value.GetOrThrow();
            
            if (interactionTargetComponent.InteractionTargetType is not InteractionTargetType.Innocent)
            {
                return;
            }
            
            var npcHatComponent = interactionTargetComponent.GetComponent<HatComponent>();
            var npcHatRendererComponent = interactionTargetComponent.GetComponent<HatRendererComponent>();
            var npcHat = npcHatComponent.Hat.GetOrThrow();
            var npcHatSettings = builtDataHolder.HatSettings.First(settings => settings.Id == npcHat.HatId);
            
            var playerHatComponent = playerObjectHolder.GameObject.GetComponent<HatComponent>();
            var playerHatRendererComponent = playerObjectHolder.GameObject.GetComponent<HatRendererComponent>();
            var playerHat = playerHatComponent.Hat.GetOrThrow();
            var playerHatSettings = builtDataHolder.HatSettings.First(settings => settings.Id == playerHat.HatId);

            npcHatComponent.Hat = playerHat;
            playerHatComponent.Hat = npcHat;
            
            npcHatRendererComponent.SpriteRenderer.sprite = playerHatSettings.Icon;
            playerHatRendererComponent.SpriteRenderer.sprite = npcHatSettings.Icon;
            sceneDataHolder.PlayerHatVisualComponent.Image.sprite = npcHatSettings.Icon;
            
            var itemsFromInnocent = npcHat.InventorySlots.Where(slot => slot.StoredItemId.IsInitialized).Select(slot => slot.StoredItemId.GetOrThrow());
            inventoryItemsHolder.Clear();
            inventoryItemsHolder.AddRange(itemsFromInnocent);

            // JUST USE IT!!!
            EventContext.Bus.Invoke(new PlayerHatSwappedEvent());
        }
    }
}