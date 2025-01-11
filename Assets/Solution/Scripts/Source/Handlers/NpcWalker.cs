using AreYouFruits.Events;
using AreYouFruits.VectorsSwizzling;
using Greg.Components;
using Greg.Data;
using Greg.Events;
using Greg.Holders;
using UnityEngine;

namespace Greg.Handlers
{
    public sealed partial class NpcWalker
    {
        [EventHandler]
        private static void Handle(
            UpdateEvent _,
            ComponentsResource componentsResource
        )
        {
            foreach (var gameObject in componentsResource.Get<WalkingNpcComponent>())
            {
                if (!CanWalk(gameObject))
                {
                    continue;
                }

                var walkingNpcComponent = gameObject.GetComponent<WalkingNpcComponent>();
                var speed = gameObject.GetComponent<CharacterSpeedComponent>().Speed;

                var targetWalkPoint = walkingNpcComponent.WalkPath[walkingNpcComponent.TargetIndex];

                if (!Mathf.Approximately((gameObject.transform.position.XY() - targetWalkPoint.Position).sqrMagnitude, 0))
                {
                    Walk(gameObject, targetWalkPoint.Position, speed);
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

        private static bool CanWalk(GameObject gameObject)
        {
            if (!gameObject.TryGetComponent(out GuardStateComponent guardStateComponent))
            {
                return true;
            }

            return guardStateComponent.State is GuardStateType.Calm;
        }

        private static void Walk(GameObject walker, Vector2 target, float speed)
        {
            var walkerTransform = walker.transform;
            var walkerRigidbody = walker.GetComponent<Rigidbody2D>();

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