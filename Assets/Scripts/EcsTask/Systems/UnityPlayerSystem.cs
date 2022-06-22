using EcsTask.Components;
using EcsTask.Unity;
using Leopotam.EcsLite;

namespace EcsTask.Systems
{
    public class UnityPlayerSystem : IEcsInitSystem, IEcsRunSystem
    {
        public void Init(EcsSystems systems)
        {
            var playerView = systems.GetShared<SharedData>().playerView;
            var world = systems.GetWorld();

            var playerPool = world.GetPool<PlayerComponent>();

            var playerEntity = world.NewEntity();
            ref var playerComponent = ref playerPool.Add(playerEntity);
            playerComponent.position = playerComponent.targetPosition = playerView.transform.position;
        }

        public void Run(EcsSystems systems)
        {
            var playerView = systems.GetShared<SharedData>().playerView;
            var world = systems.GetWorld();

            var playerFilter = world.Filter<PlayerComponent>().End();
            var playerPool = world.GetPool<PlayerComponent>();

            foreach (var entity in playerFilter)
            {
                ref var playerComponent = ref playerPool.Get(entity);
                playerComponent.position = playerView.Position;
                playerView.SetDestination(playerComponent.targetPosition);
            }
        }
    }
}