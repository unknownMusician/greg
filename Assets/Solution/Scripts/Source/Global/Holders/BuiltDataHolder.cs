using AreYouFruits.Events;
using UnityEngine;

namespace Greg.Global.Holders
{
    [ReadonlyResourceAccess]
    [CreateAssetMenu(menuName = "BuiltDataHolder", fileName = "BuiltDataHolder", order = 0)]
    public sealed class BuiltDataHolder : ScriptableObject
    {
        [field: SerializeField] public float PlayerSpeed { get; private set; }
    }
}
