using System.Collections.Generic;
using AreYouFruits.Collections;
using AreYouFruits.Events;
using AreYouFruits.Nullability;
using Greg.Components;
using Greg.Data;
using Greg.Events;
using Greg.Global.Api;
using Greg.Global.Holders;

namespace Greg.Handlers
{
    public sealed partial class CharacterHatInitializer
    {
        [EventHandler]
        private static void Handle(
            CharacterSpawnedEvent @event,
            BuiltDataHolder builtDataHolder,
            SceneDataHolder sceneDataHolder
        )
        {
            if (@event.CharacterType == CharacterType.Guard)
            {
                return;
            }
            
            var hatComponent = @event.GameObject.GetComponent<HatComponent>();

            var hatSettings = builtDataHolder.HatSettings.GetRandomElement<HatSettings, List<HatSettings>>();

            var inventorySlots = new List<InventorySlot>();

            for (int i = 0; i < hatSettings.MaxSlotsAmount; i++)
            {
                inventorySlots.Add(new InventorySlot { StoredItemId = Optional.None() });
            }

            hatComponent.Hat = new Hat()
            {
                HatId = hatSettings.Id,
                InventorySlots = inventorySlots
            };
            
            var hatRendererComponent = @event.GameObject.GetComponent<HatRendererComponent>();
            hatRendererComponent.SpriteRenderer.sprite = hatSettings.Icon;

            if (@event.CharacterType == CharacterType.Player)
            {
                sceneDataHolder.PlayerHatVisualComponent.Image.sprite = hatSettings.Icon;
                EventContext.Bus.Invoke(new InventoryChangedEvent());
            }
        }
    }
}