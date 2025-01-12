using AreYouFruits.Events;
using AreYouFruits.VectorsSwizzling;
using Greg.Components;
using Greg.Data;
using Greg.Events;
using Greg.Global.Holders;
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
            BuiltDataHolder builtDataHolder,
            SceneDataHolder sceneDataHolder,
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

                if (!walkingNpcComponent.TargetIndex.TryGet(out var targetIndex))
                {
                    targetIndex = Random.Range(0, sceneDataHolder.WalkPointsParent.childCount);
                    walkingNpcComponent.TargetIndex = targetIndex;
                }

                var targetWalkPoint = sceneDataHolder.WalkPointsParent.GetChild(targetIndex).position;

                if (!Mathf.Approximately((gameObject.transform.position.XY() - targetWalkPoint.XY()).sqrMagnitude, 0))
                {
                    Walk(gameObject, targetWalkPoint, speed, pathFinderHolder);
                    continue;
                }
                
                walkingNpcComponent.WaitedTime += Time.deltaTime;
                if (walkingNpcComponent.WaitedTime >= walkingNpcComponent.NeededTime)
                {
                    var index = Random.Range(0, sceneDataHolder.WalkPointsParent.childCount);

                    walkingNpcComponent.TargetIndex = index;
                    walkingNpcComponent.WaitedTime = 0;
                    walkingNpcComponent.NeededTime =
                        Random.Range(builtDataHolder.WalkDelay.Min, builtDataHolder.WalkDelay.Max);
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