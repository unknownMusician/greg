using AreYouFruits.Physics.Unity;
using Greg.Components;
using Greg.Events;
using Greg.Global.Api;
using UnityEngine;

namespace Greg.Utils
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class InteractionTargetTriggerEventSender : MonoBehaviour
    {
        [SerializeField] private Trigger2DListener triggerListener;
        
        private void Awake()
        {
            triggerListener.OnEnter += HandleTriggerEnter;
            triggerListener.OnExit += HandleTriggerExit;
        }

        private void OnDestroy()
        {
            triggerListener.OnEnter -= HandleTriggerEnter;
            triggerListener.OnExit -= HandleTriggerExit;
        }

        private void HandleTriggerEnter(Collider2D collider)
        {
            if (!collider.TryGetComponent(out InteractionTargetComponent interactionTargetComponent))
            {
                return;
            }

            EventContext.Bus.Invoke(new PlayerTriggeredInteractionTargetEvent
            {
                InteractionTargetComponent = interactionTargetComponent
            });
        }
        
        private void HandleTriggerExit(Collider2D collider)
        {
            if (!collider.TryGetComponent(out InteractionTargetComponent interactionTargetComponent))
            {
                return;
            }
            
            EventContext.Bus.Invoke(new PlayerUntriggeredInteractionTargetEvent
            {
                InteractionTargetComponent = interactionTargetComponent
            });
        }
    }
}