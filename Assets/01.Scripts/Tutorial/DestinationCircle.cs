using System;
using DG.Tweening;
using UnityEngine;

namespace BSM
{
    public class DestinationCircle : MonoBehaviour
    {
        public event Action PlayerArrived;

        private void Start()
        {
            transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
            transform.DORotate(new Vector3(0f, 180f, 0f), 2f, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Incremental)
                .SetEase(Ease.Linear);
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayerArrived?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
