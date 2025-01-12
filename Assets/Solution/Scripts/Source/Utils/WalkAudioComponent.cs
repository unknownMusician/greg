using AreYouFruits.Collections;
using UnityEngine;

namespace Greg.Utils
{
    public sealed class WalkAudioComponent : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip[] audioClips;
        
        public void PlayRandomWalkSoundFromAnimation()
        {
            var clip = audioClips.GetRandomElement();

            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}