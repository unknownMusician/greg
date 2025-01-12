using AreYouFruits.Events;
using Greg.Data;
using Greg.Events;
using Greg.Global.Api;
using Greg.Global.Holders;
using Greg.Holders;
using UnityEngine;

namespace Greg.Handlers
{
    public sealed partial class CharactersInitialSpawner
    {
        [EventHandler]
        private static void Handle(
            StartEvent _,
            SceneDataHolder sceneDataHolder,
            BuiltDataHolder builtDataHolder,
            NpcHolder npcHolder
        )
        {
            var playerGameObject = Object.Instantiate(builtDataHolder.PlayerPrefab, sceneDataHolder.PlayerSpawnPoint.position, Quaternion.identity);
            EventContext.Bus.Invoke(new CharacterSpawnedEvent
            {
                CharacterType = CharacterType.Player, 
                GameObject = playerGameObject
            });
            
            foreach (var spawnPosition in sceneDataHolder.InnocentSpawnPoints)
            {
                var innocentGameObject = Object.Instantiate(builtDataHolder.InnocentPrefab, spawnPosition.position, Quaternion.identity);
                npcHolder.Values.Add(innocentGameObject);
                EventContext.Bus.Invoke(new CharacterSpawnedEvent
                {
                    CharacterType = CharacterType.Innocent,
                    GameObject = innocentGameObject
                });
            }
            
            foreach (var spawnPosition in sceneDataHolder.SafemanSpawnPoints)
            {
                var safemanGameObject = Object.Instantiate(builtDataHolder.SafemanPrefab, spawnPosition.position, Quaternion.identity);
                npcHolder.Values.Add(safemanGameObject);
                EventContext.Bus.Invoke(new CharacterSpawnedEvent
                {
                    CharacterType = CharacterType.Safeman,
                    GameObject = safemanGameObject
                });
            }
            
            foreach (var spawnPosition in sceneDataHolder.GuardSpawnPoints)
            {
                var guardGameObject = Object.Instantiate(builtDataHolder.GuardPrefab, spawnPosition.position, Quaternion.identity);
                npcHolder.Values.Add(guardGameObject);
                EventContext.Bus.Invoke(new CharacterSpawnedEvent
                {
                    CharacterType = CharacterType.Guard,
                    GameObject = guardGameObject
                });
            }
        }
    }
}