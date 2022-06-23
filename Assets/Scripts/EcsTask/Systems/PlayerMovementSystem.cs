using EcsTask.Components;
using EcsTask.Unity;
using Leopotam.EcsLite;
using UnityEngine;

namespace EcsTask.Systems
{
    public class PlayerMovementSystem : IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var world = systems.GetWorld();

            var playerFilter = world.Filter<PlayerComponent>().Inc<TransformComponent>().End();
            var mouseInputFilter = world.Filter<MouseInputComponent>().End();

            var transformPool = world.GetPool<TransformComponent>();
            var mouseInputPool = world.GetPool<MouseInputComponent>();

            var movementSpeed = systems.GetShared<SharedData>().playerSpeed;
            var deltaTime = systems.GetShared<SharedData>().deltaTime;

            foreach (var playerEntity in playerFilter)
            {
                ref var playerTransformComponent = ref transformPool.Get(playerEntity);
                foreach (var mouseInput in mouseInputFilter)
                {
                    ref var mouseInputComponent = ref mouseInputPool.Get(mouseInput);
                    if (!mouseInputComponent.isSet)
                        continue;

                    Vector3 startPosition = playerTransformComponent.position;
                    Vector3 targetPosition = mouseInputComponent.position;
                    targetPosition.y = startPosition.y;
                    float maxDistanceDelta = movementSpeed * deltaTime;
                    MovePlayer(ref playerTransformComponent, targetPosition, maxDistanceDelta);
                }
            }
        }

        private static void MovePlayer(ref TransformComponent transform, Vector3 target, float maxDistanceDelta)
        {
            var startPosition = transform.position;
            Vector3 deltaPosition = target - startPosition;
            if (deltaPosition.magnitude < maxDistanceDelta)
            {
                transform.position = target;
            }
            else
            {
                transform.position += deltaPosition.normalized * maxDistanceDelta;
            }
        }
    }
}