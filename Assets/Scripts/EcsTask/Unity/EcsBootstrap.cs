using EcsTask.Components;
using EcsTask.Systems;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace EcsTask.Unity
{
    public class SharedData
    {
    }

    public class EcsBootstrap : MonoBehaviour
    {
        [Inject] public PlayerView playerView;

        [Inject] private EcsWorld _ecsWorld;
        [Inject] private SharedData _sharedData;
        [Inject] private MouseInputSystem _mouseInputSystem;
        [Inject] private PlayerMovementSystem _playerMovementSystem;
        [Inject] private ButtonDoorLogicSystem _buttonDoorLogicSystem;

        private EcsSystems _systems;

        void Start()
        {
            var playerEntity = _ecsWorld.NewEntity();
            ref var playerComponent = ref _ecsWorld.GetPool<PlayerComponent>().Add(playerEntity);
            playerComponent.view = playerView;

            _systems = new EcsSystems(_ecsWorld, _sharedData);
            _systems
                .Add(_mouseInputSystem)
                .Add(_playerMovementSystem)
                .Add(_buttonDoorLogicSystem)
                .Init();
        }

        void Update()
        {
            _systems.Run();
        }

        private void OnDestroy()
        {
            _systems.Destroy();
        }
    }
}