using UnityEngine;

namespace Solution.Scripts.Source.Components
{
    public sealed class GuardNotifyComponent : MonoBehaviour
    {
        [field: SerializeField] public GameObject ExclamationMark { get; private set; }
    }
}