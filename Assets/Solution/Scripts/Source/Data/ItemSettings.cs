using System;
using UnityEngine;

namespace Greg.Data
{
    [Serializable]
    public class ItemSettings
    {
        [field: SerializeField] public uint Id { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public ItemSourceType SourceType { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
        [field: SerializeField] public uint Difficulty { get; private set; }
    }
}