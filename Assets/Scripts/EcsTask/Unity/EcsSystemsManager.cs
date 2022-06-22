using EcsTask.Systems;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace EcsTask.Unity
{
    public class SharedData
    {
        [Inject] public PlayerView playerView;
    }

    public class EcsSystemsManager : MonoBehaviour
    {
        [Inject] private EcsWorld _ecsWorld;
        [Inject] private SharedData _sharedData;
        [Inject] private MouseInputSystem _mouseInputSystem;
        [Inject] private UnityPlayerSystem _unityPlayerSystem;
        [Inject] private PlayerMovementSystem _playerMovementSystem;
        [Inject] private ButtonLogicSystem _buttonLogicSystem;

        private EcsSystems _systems;

        void Start()
        {
            _systems = new EcsSystems(_ecsWorld, _sharedData);
            _systems
                .Add(_mouseInputSystem)
                .Add(_unityPlayerSystem)
                .Add(_playerMovementSystem)
                .Add(_buttonLogicSystem)
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