using EcsTask.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace EcsTask.Systems
{
    public class MouseInputSystem : IEcsInitSystem, IEcsRunSystem
    {
        public void Init(EcsSystems systems)
        {
            var inputEntity = systems.GetWorld().NewEntity();
            systems.GetWorld().GetPool<MouseInputComponent>().Add(inputEntity);
        }

        public void Run(EcsSystems systems)
        {
            var world = systems.GetWorld();

            var filter = world.Filter<MouseInputComponent>().End();
            var playerMouseInputPool = world.GetPool<MouseInputComponent>();

            if (Input.GetMouseButton(0))
            {
                Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(mouseRay, out RaycastHit hitInfo);
                Vector3 hitPoint = hitInfo.point;

                foreach (var entity in filter)
                {
                    ref MouseInputComponent inputComponent = ref playerMouseInputPool.Get(entity);
                    inputComponent.position = hitPoint;
                    inputComponent.isSet = true;
                }
            }
        }
    }
}