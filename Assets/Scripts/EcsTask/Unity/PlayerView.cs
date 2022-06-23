using EcsTask.Components;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace EcsTask.Unity
{
    public class PlayerView : MonoBehaviour
    {
        [Inject] private EcsWorld _ecsWorld;

        private void Update()
        {
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            var playerFilter = _ecsWorld.Filter<PlayerComponent>().Inc<TransformComponent>().End();
            var transformPool = _ecsWorld.GetPool<TransformComponent>();

            foreach (var playerEntity in playerFilter)
            {
                ref var playerTransform = ref transformPool.Get(playerEntity);
                if (!playerTransform.isSetFromScene)
                {
                    playerTransform.position = transform.position;
                    playerTransform.isSetFromScene = true;
                }
                else
                {
                    transform.position = playerTransform.position;
                }
            }
        }
    }
}