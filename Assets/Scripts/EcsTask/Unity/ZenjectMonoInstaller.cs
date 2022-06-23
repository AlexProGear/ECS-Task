using EcsTask.Systems;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace EcsTask.Unity
{
    public class ZenjectMonoInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerView>().FromComponentOn(GameObject.FindWithTag("Player")).AsSingle();

            Container.Bind<EcsWorld>().FromNew().AsSingle();
            Container.Bind<SharedData>().FromNew().AsSingle();
            Container.Bind<UnityPlayerSystem>().FromNew().AsSingle();
            Container.Bind<MouseInputSystem>().FromNew().AsSingle();
            Container.Bind<PlayerMovementSystem>().FromNew().AsSingle();
            Container.Bind<ButtonDoorLogicSystem>().FromNew().AsSingle();
        }
    }
}