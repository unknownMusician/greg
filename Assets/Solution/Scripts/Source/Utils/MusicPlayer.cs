using AreYouFruits.Events;
using Greg.Components;
using Greg.Data;
using Greg.Holders;
using Unity.Mathematics.Geometry;
using UnityEngine;

namespace Greg.Utils
{
    public sealed class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource calm;
        [SerializeField] private AudioSource aggressive;
        [SerializeField] private float maxVolume;
        [SerializeField] private float speed;

        private float aggressiveProgress;
        
        private void Update()
        {
            if (!ResourcesLocator.TryGet<ComponentsResource>().TryGet(out var components))
            {
                return;
            }

            var isAggressive = false;
            
            foreach (var o in components.Get<GuardStateComponent>())
            {
                if (o.GetComponent<GuardStateComponent>().State == GuardStateType.Aggressive)
                {
                    isAggressive = true;
                }
            }

            if (isAggressive)
            {
                aggressiveProgress += speed * Time.deltaTime;
            }
            else
            {
                aggressiveProgress -= speed * Time.deltaTime;
            }
            
            UpdateState();
        }

        private void UpdateState()
        {
            calm.volume = Mathf.Lerp(maxVolume, 0, aggressiveProgress);
            aggressive.volume = Mathf.Lerp(0, maxVolume, aggressiveProgress);
        }
    }
}