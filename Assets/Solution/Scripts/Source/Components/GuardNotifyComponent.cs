using UnityEngine;

namespace Greg.Components
{
    public sealed class GuardNotifyComponent : MonoBehaviour
    {
        [field: SerializeField] public GameObject ExclamationMark { get; private set; }
    }
}