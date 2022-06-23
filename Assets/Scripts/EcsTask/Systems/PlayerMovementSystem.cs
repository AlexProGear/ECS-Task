using EcsTask.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace EcsTask.Systems
{
    public class PlayerMovementSystem : IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var world = systems.GetWorld();

            var playerFilter = world.Filter<PlayerComponent>().End();
            var mouseInputFilter = world.Filter<MouseInputComponent>().End();

            var playerPool = world.GetPool<PlayerComponent>();
            var mouseInputPool = world.GetPool<MouseInputComponent>();

            foreach (var playerEntity in playerFilter)
            {
                ref var playerComponent = ref playerPool.Get(playerEntity);
                foreach (var mouseInput in mouseInputFilter)
                {
                    ref var mouseInputComponent = ref mouseInputPool.Get(mouseInput);
                    if (!mouseInputComponent.isSet)
                        continue;
                    Vector3 endPosition = mouseInputComponent.position;
                    playerComponent.view.SetDestination(endPosition);
                }
            }
        }
    }
}