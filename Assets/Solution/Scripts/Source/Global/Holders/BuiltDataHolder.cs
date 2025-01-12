using System.Collections.Generic;
using AreYouFruits.Events;
using Greg.Data;
using Greg.Utils;
using UnityEngine;

namespace Greg.Global.Holders
{
    [ReadonlyResourceAccess]
    [CreateAssetMenu(menuName = "BuiltDataHolder", fileName = "BuiltDataHolder", order = 0)]
    public sealed class BuiltDataHolder : ScriptableObject
    {
        [Header("Layering")]
        [field: SerializeField] public float HeightDepthMultiplier { get; private set; } = 1;
        
        [Header("Stealing")]
        [field: SerializeField] public float StealDistance { get; private set; }
        
        [Header("Inventory")]
        [field: SerializeField] public int InventoryCapacity { get; private set; }
        [field: SerializeField] public List<ItemSettings> ItemSettings { get; private set; }
        [field: SerializeField] public List<HatSettings> HatSettings { get; private set; }
        
        [Header("Guards")]
        [field: SerializeField] public float GuardLookDistance { get; private set; }
        [field: SerializeField] public float GuardLookAngle { get; private set; }
        [field: SerializeField] public float CatchDistance { get; private set; }
        
        [Header("Innocents")]
        [field: SerializeField] public float PocketItemSpawnProbability { get; private set; }
        
        [Header("Customization")]
        [field: SerializeField] public List<Sprite> HeadSprites { get; private set; }
        [field: SerializeField] public List<Sprite> BodySprites { get; private set; }
        [field: SerializeField] public List<Sprite> LegsSprites { get; private set; }
        
        [Header("Pathfinding")]
        [field: SerializeField] public float PathfindingGridSize { get; private set; }
        
        [Header("Prefabs")]
        [field: SerializeField] public GameObject PlayerPrefab { get; private set; }
        [field: SerializeField] public GameObject SafemanPrefab { get; private set; }
        [field: SerializeField] public GameObject InnocentPrefab { get; private set; }
        [field: SerializeField] public GameObject GuardPrefab { get; private set; }
        
        [Header("Sfx")]
        [field: SerializeField] public InspectorDictionary<CrowdSfxCharacterType, AudioClip[]> CrowdSfx { get; private set; }
        
        [Header("Path")]
        [field: SerializeField] public PathFinderRoom[] Rooms { get; private set; }
    }
}
