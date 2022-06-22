using System.Linq;
using EcsTask.Components;
using EcsTask.Unity;
using Leopotam.EcsLite;
using Zenject;

namespace EcsTask.Systems
{
    public class ButtonLogicSystem : IEcsRunSystem
    {
        [Inject] private DoorView[] _doorViews;

        public void Run(EcsSystems systems)
        {
            var world = systems.GetWorld();
            var buttonPressFilter = world.Filter<ButtonPressComponent>().End();
            var buttonPressPool = world.GetPool<ButtonPressComponent>();

            foreach (var entity in buttonPressFilter)
            {
                ref var buttonPressComponent = ref buttonPressPool.Get(entity);
                var buttonId = buttonPressComponent.id;

                foreach (var door in _doorViews.Where(door => door.id == buttonId))
                {
                    if (buttonPressComponent.isPressed)
                    {
                        door.StartMoving();
                    }
                    else
                    {
                        door.StopMoving();
                    }
                }

                world.DelEntity(entity);
            }
        }
    }
}