using System;
using EcsTask.Systems;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace EcsTask.Unity
{
    public class SharedData
    {
        public float playerSpeed = 5f;
        public float deltaTime;
    }

    public class EcsBootstrap : IInitializable, ITickable, IDisposable
    {
        [Inject] private EcsWorld _ecsWorld;
        [Inject] private SharedData _sharedData;
        [Inject] private PlayerInitSystem _playerInitSystem;
        [Inject] private MouseInputSystem _mouseInputSystem;
        [Inject] private PlayerMovementSystem _playerMovementSystem;
        [Inject] private ButtonDoorLogicSystem _buttonDoorLogicSystem;

        private EcsSystems _systems;

        public void Initialize()
        {
            _systems = new EcsSystems(_ecsWorld, _sharedData);
            _systems
                .Add(_playerInitSystem)
                .Add(_mouseInputSystem)
                .Add(_playerMovementSystem)
                .Add(_buttonDoorLogicSystem)
                .Init();
        }

        public void Tick()
        {
            _systems.GetShared<SharedData>().deltaTime = Time.deltaTime;
            _systems.Run();
        }

        public void Dispose()
        {
            _systems.Destroy();
        }
    }
}