using AreYouFruits.Events;
using AreYouFruits.VectorsSwizzling;
using Greg.Components;
using Greg.Data;
using Greg.Events;
using Greg.Holders;
using Greg.Utils;
using UnityEngine;

namespace Greg.Handlers
{
    public sealed partial class NpcWalker
    {
        [EventHandler]
        private static void Handle(
            UpdateEvent _,
            ComponentsResource componentsResource,
            PlayerObjectHolder playerObjectHolder,
            PathFinderHolder pathFinderHolder
        )
        {
            foreach (var gameObject in componentsResource.Get<WalkingNpcComponent>())
            {
                var speed = gameObject.GetComponent<CharacterSpeedComponent>().Speed;

                if (gameObject.TryGetComponent(out GuardStateComponent guardStateComponent))
                {
                    if (guardStateComponent.State == GuardStateType.Aggressive)
                    {
                        Walk(gameObject, playerObjectHolder.GameObject.transform.position, speed, pathFinderHolder);
                    }
                    else if (guardStateComponent.State == GuardStateType.Investigative)
                    {
                        gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
                    }
                    
                    continue;
                }
                
                var walkingNpcComponent = gameObject.GetComponent<WalkingNpcComponent>();

                var targetWalkPoint = walkingNpcComponent.WalkPath[walkingNpcComponent.TargetIndex];

                if (!Mathf.Approximately((gameObject.transform.position.XY() - targetWalkPoint.Position).sqrMagnitude, 0))
                {
                    Walk(gameObject, targetWalkPoint.Position, speed, pathFinderHolder);
                    continue;
                }
                
                walkingNpcComponent.WaitedTime += Time.deltaTime;
                if (walkingNpcComponent.WaitedTime > targetWalkPoint.WaitDuration)
                {
                    walkingNpcComponent.TargetIndex =
                        (walkingNpcComponent.TargetIndex + 1) % walkingNpcComponent.WalkPath.Count;
                    walkingNpcComponent.WaitedTime = 0;
                }
            }
        }

        private static void Walk(GameObject walker, Vector2 target, float speed, PathFinderHolder pathFinderHolder)
        {
            var walkerTransform = walker.transform;
            var walkerRigidbody = walker.GetComponent<Rigidbody2D>();

            target = PathFinderUtils.GetTarget(pathFinderHolder, walkerTransform.position, target);
            
            var direction = target - walkerTransform.position.XY();

            var maxDistance = speed * Time.deltaTime;

            if (direction.sqrMagnitude <= maxDistance * maxDistance)
            {
                walkerTransform.position = target;
                
                walkerRigidbody.bodyType = RigidbodyType2D.Kinematic;
                walkerRigidbody.linearVelocity = Vector2.zero;
            }
            else
            {
                walkerRigidbody.bodyType = RigidbodyType2D.Dynamic;
                walkerRigidbody.linearVelocity = direction.normalized * speed;
            }
        }
    }
}