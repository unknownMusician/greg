using Greg.Utils.TagSearcher;
using UnityEngine;

namespace Greg.Utils
{
    [ScriptTag(ArchitectureTag.Component)]
    public sealed class BigObjectPart : MonoBehaviour
    {
        [field: SerializeField] public GameObject BigObject { get; private set; }

        public static GameObject GetBigObjectOrSame(GameObject potentialPart)
        {
            if (potentialPart.TryGetComponent(out BigObjectPart bigObjectPart))
            {
                return bigObjectPart.BigObject;
            }

            return potentialPart;
        }
    }
}
