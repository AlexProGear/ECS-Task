using DG.Tweening;
using UnityEngine;

namespace EcsTask.Unity
{
    public class DoorView : MonoBehaviour
    {
        [SerializeField] private Vector3 moveOffset;
        [SerializeField] private float moveTime;
        [field: SerializeField] public string id { get; private set; }

        private Tween _doorTween;

        private void Awake()
        {
            _doorTween = transform.DOLocalMove(moveOffset, moveTime)
                .SetRelative(true)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);

            _doorTween.Pause();
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