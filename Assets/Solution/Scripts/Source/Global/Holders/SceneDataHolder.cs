using System.Collections.Generic;
using Greg.Components;
using Greg.Utils.TagSearcher;
using TMPro;
using UnityEngine;

namespace Greg.Global.Holders
{
    [ScriptTag(ArchitectureTag.HolderResource)]
    public sealed class SceneDataHolder : MonoBehaviour
    {
        [field: SerializeField] public GameObject EnvironmentContainer { get; private set; }
        [field: SerializeField] public Transform InventoryCellsParent { get; private set; }
        [field: SerializeField] public Transform PlayerSpawnPoint { get; private set; }
        [field: SerializeField] public List<Transform> InnocentSpawnPoints { get; private set; }
        [field: SerializeField] public List<Transform> GuardSpawnPoints { get; private set; }
        [field: SerializeField] public List<Transform> SafemanSpawnPoints { get; private set; }
        [field: SerializeField] public GameObject GamePausedWindow { get; private set; }
        [field: SerializeField] public GameObject ResultWindow { get; private set; }
        [field: SerializeField] public GameObject PauseButton { get; private set; }
        [field: SerializeField] public GameObject MuteButton { get; private set; }
        [field: SerializeField] public Bounds LevelBounds { get; private set; }
        [field: SerializeField] public PlayerHatVisualComponent PlayerHatVisualComponent { get; private set; }
        [field: SerializeField] public CollectedMoneyTextComponent CollectedMoneyTextComponent { get; private set; }
        [field: SerializeField] public LevelMoneyTextComponent LevelMoneyTextComponent { get; private set; }
        [field: SerializeField] public InventoryMoneyTextComponent InventoryMoneyTextComponent { get; private set; }
        [field: SerializeField] public TextMeshProUGUI ResultMoneyText { get; private set; }
        [field: SerializeField] public TextMeshProUGUI ResultTimeText { get; private set; }
    }
}
