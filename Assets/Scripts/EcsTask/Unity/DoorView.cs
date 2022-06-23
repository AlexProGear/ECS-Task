using DG.Tweening;
using EcsTask.Components;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace EcsTask.Unity
{
    public class DoorView : MonoBehaviour
    {
        [SerializeField] private Vector3 moveOffset;
        [SerializeField] private float moveTime;
        [field: SerializeField] public string id { get; private set; }

        [Inject] private EcsWorld _ecsWorld;

        private Tween _doorTween;

        private void Awake()
        {
            _doorTween = transform.DOLocalMove(moveOffset, moveTime)
                .SetRelative(true)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);

            _doorTween.Pause();

            var entity = _ecsWorld.NewEntity();
            ref var doorComponent = ref _ecsWorld.GetPool<DoorComponent>().Add(entity);
            doorComponent.view = this;
        }

        private void OnDestroy()
        {
            _doorTween.Kill();
        }

        public void StartMoving()
        {
            if (_doorTween.IsPlaying())
                return;

            _doorTween.Play();
        }

        public void StopMoving()
        {
            _doorTween.Pause();
        }
    }
}