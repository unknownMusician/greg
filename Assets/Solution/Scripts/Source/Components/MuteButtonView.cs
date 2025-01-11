using UnityEngine;

namespace Greg.Components
{
    public sealed class MuteButtonView : MonoBehaviour
    {
        [field: SerializeField] public GameObject SoundOnImage { get; private set; }
        [field: SerializeField] public GameObject SoundOffImage { get; private set; }
    }
}