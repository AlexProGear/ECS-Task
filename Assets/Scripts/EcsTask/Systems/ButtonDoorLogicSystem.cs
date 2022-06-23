using EcsTask.Components;
using Leopotam.EcsLite;

namespace EcsTask.Systems
{
    public class ButtonDoorLogicSystem : IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var world = systems.GetWorld();

            var buttonPressFilter = world.Filter<ButtonPressComponent>().End();
            var doorFilter = world.Filter<DoorComponent>().End();

            var buttonPressPool = world.GetPool<ButtonPressComponent>();
            var doorPool = world.GetPool<DoorComponent>();

            foreach (var entityButtonPress in buttonPressFilter)
            {
                ref var buttonPressComponent = ref buttonPressPool.Get(entityButtonPress);
                var buttonId = buttonPressComponent.id;

                foreach (var entityDoor in doorFilter)
                {
                    ref var doorComponent = ref doorPool.Get(entityDoor);
                    if (doorComponent.view.id != buttonId)
                    {
                        continue;
                    }

                    if (buttonPressComponent.isPressed)
                    {
                        doorComponent.view.StartMoving();
                    }
                    else
                    {
                        doorComponent.view.StopMoving();
                    }
                }

                world.DelEntity(entityButtonPress);
            }
        }
    }
}