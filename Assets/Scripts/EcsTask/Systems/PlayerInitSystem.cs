using EcsTask.Components;
using Leopotam.EcsLite;

namespace EcsTask.Systems
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        public void Init(EcsSystems systems)
        {
            var world = systems.GetWorld();

            var playerEntity = world.NewEntity();
            world.GetPool<PlayerComponent>().Add(playerEntity);
            world.GetPool<TransformComponent>().Add(playerEntity);
        }
    }
}