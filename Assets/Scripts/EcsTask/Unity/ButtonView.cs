using EcsTask.Components;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace EcsTask.Unity
{
    public class ButtonView : MonoBehaviour
    {
        [SerializeField] private string id;

        [Inject] private EcsWorld _ecsWorld;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;

            UpdateButtonState(true);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;

            UpdateButtonState(false);
        }

        private void UpdateButtonState(bool pressed)
        {
            var buttonPressPool = _ecsWorld.GetPool<ButtonPressComponent>();

            var entity = _ecsWorld.NewEntity();
            ref var buttonPressComponent = ref buttonPressPool.Add(entity);

            buttonPressComponent.id = id;
            buttonPressComponent.isPressed = pressed;
        }
    }
}