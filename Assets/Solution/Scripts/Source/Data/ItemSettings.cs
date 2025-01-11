using System;
using UnityEngine;

namespace Greg.Data
{
    [Serializable]
    public class ItemSettings
    {
        [field: SerializeField] public uint Id { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}