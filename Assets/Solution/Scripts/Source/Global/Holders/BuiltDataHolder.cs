using System.Collections.Generic;
using AreYouFruits.Events;
using Greg.Data;
using UnityEngine;

namespace Greg.Global.Holders
{
    [ReadonlyResourceAccess]
    [CreateAssetMenu(menuName = "BuiltDataHolder", fileName = "BuiltDataHolder", order = 0)]
    public sealed class BuiltDataHolder : ScriptableObject
    {
        [Header("Movement")]
        [field: SerializeField] public float PlayerSpeed { get; private set; }
        
        [Header("Stealing")]
        [field: SerializeField] public float StealDistance { get; private set; }
        
        [Header("Inventory")]
        [field: SerializeField] public int InventoryCapacity { get; private set; }
        [field: SerializeField] public GameObject InventoryCellPrefab { get; private set; }
        [field: SerializeField] public List<ItemSettings> ItemSettings { get; private set; }
        [field: SerializeField] public List<HatSettings> HatSettings { get; private set; }
        
        [Header("Guards")]
        [field: SerializeField] public float GuardLookDistance { get; private set; }
        [field: SerializeField] public float GuardLookAngle { get; private set; }
        
        [Header("Innocents")]
        [field: SerializeField] public float PocketItemSpawnProbability { get; private set; }
    }
}
