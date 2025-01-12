using AreYouFruits.Collections;
using AreYouFruits.Events;
using Greg.Data;
using Greg.Events;
using Greg.Utils;

namespace Greg.Handlers
{
    public sealed partial class CharacterCrowdSfxTypeInitializer
    {
        [EventHandler]
        private static void Handle(
            CharacterSpawnedEvent @event
        )
        {
            if (@event.CharacterType != CharacterType.Innocent)
            {
                return;
            }
            
            var crowdAudio = @event.GameObject.GetComponent<CrowdAudioComponent>();

            crowdAudio.CrowdSfxType = Consts.CrowdSfxCharacterTypeValues.GetRandomElement();
        }
    }
}