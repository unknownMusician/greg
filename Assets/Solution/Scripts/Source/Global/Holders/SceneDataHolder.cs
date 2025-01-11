using Greg.Utils.TagSearcher;
using UnityEngine;

namespace Greg.Global.Holders
{
    [ScriptTag(ArchitectureTag.HolderResource)]
    public sealed class SceneDataHolder : MonoBehaviour
    {
        [field: SerializeField] public GameObject EnvironmentContainer { get; private set; }
        [field: SerializeField] public GameObject Player { get; private set; }
        [field: SerializeField] public Transform InventoryCellsParent { get; private set; }
        [field: SerializeField] public GameObject GamePausedWindow { get; private set; }
        [field: SerializeField] public GameObject PauseButton { get; private set; }
    }
}
