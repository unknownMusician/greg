using System.Collections.Generic;
using AreYouFruits.Events;
using Solution.Scripts.Source.Data;
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
    }
}
