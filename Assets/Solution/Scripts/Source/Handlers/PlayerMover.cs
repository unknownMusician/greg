using AreYouFruits.Events;
using Greg.Components;
using Greg.Events;
using Greg.Holders;
using UnityEngine;

namespace Greg.Handlers
{
    public sealed partial class PlayerMover
    {
        [EventHandler]
        private static void Handle(
            PlayerMoveInputEvent @event,
            PlayerObjectHolder playerObjectHolder,
            ComponentsResource componentsResource
        )
        {
            foreach (var gameObject in componentsResource.Get<PlayerComponent>())
            {
                var speed = gameObject.GetComponent<CharacterSpeedComponent>().Speed;

                var rigidbody = playerObjectHolder.GameObject.GetComponent<Rigidbody2D>();
                
                rigidbody.linearVelocity = @event.Direction * speed;
            }
        }
    }
}