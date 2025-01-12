using System;
using AreYouFruits.Collections;
using AreYouFruits.Events;
using Greg.Data;
using Greg.Global.Holders;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Greg.Utils
{
    public sealed class CrowdAudioComponent : MonoBehaviour
    {
        public CrowdSfxCharacterType CrowdSfxType;
        
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private float delay;

        private float timer;

        private void Start()
        {
            timer = Random.value * delay;
        }

        private void Update()
        {
            if (audioSource.isPlaying)
            {
                return;
            }

            if (timer == 0)
            {
                timer = Random.value * delay;
                StartPlayingRandom();
                return;
            }

            timer = Mathf.Max(0, timer - Time.deltaTime);
        }

        private void StartPlayingRandom()
        {
            var builtDataHolder = ResourcesLocator.Get<BuiltDataHolder>();
            
            audioSource.clip = builtDataHolder.CrowdSfx.Data[CrowdSfxType].GetRandomElement();
            audioSource.Play();
        }
    }
}