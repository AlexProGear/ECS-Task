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

    public class EcsBootstrap : MonoBehaviour
    {
        [Inject] private EcsWorld _ecsWorld;
        [Inject] private SharedData _sharedData;
        [Inject] private PlayerInitSystem _playerInitSystem;
        [Inject] private MouseInputSystem _mouseInputSystem;
        [Inject] private PlayerMovementSystem _playerMovementSystem;
        [Inject] private ButtonDoorLogicSystem _buttonDoorLogicSystem;

        private EcsSystems _systems;

        void Start()
        {
            _systems = new EcsSystems(_ecsWorld, _sharedData);
            _systems
                .Add(_playerInitSystem)
                .Add(_mouseInputSystem)
                .Add(_playerMovementSystem)
                .Add(_buttonDoorLogicSystem)
                .Init();
        }

        void Update()
        {
            _systems.GetShared<SharedData>().deltaTime = Time.deltaTime;
            _systems.Run();
        }

        private void OnDestroy()
        {
            _systems.Destroy();
        }
    }
}