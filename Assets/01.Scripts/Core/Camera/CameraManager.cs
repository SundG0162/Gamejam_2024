using DG.Tweening;
using SSH.Spawn;
using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace BSM.Core.Cameras
{
    public class CameraManager : MonoSingleton<CameraManager>
    {
        [field: SerializeField]
        public CinemachineCamera CurrentCam { get; private set; }
        private CinemachineBasicMultiChannelPerlin _multiChannelPerlin;
        private Tween _tiltTween;
        private Sequence _shakeSequence;

        private void Awake()
        {
            _multiChannelPerlin = CurrentCam.GetComponent<CinemachineBasicMultiChannelPerlin>();
        }

        public void ShakeCamera(float amplitude, float frequency, float time, Ease ease = Ease.Linear)
        {
            if (_shakeSequence != null && _shakeSequence.IsActive())
                _shakeSequence.Kill();

            _shakeSequence = DOTween.Sequence();

            _multiChannelPerlin.AmplitudeGain = amplitude;
            _multiChannelPerlin.FrequencyGain = frequency;

            _shakeSequence
                .Append(DOTween.To(() => _multiChannelPerlin.AmplitudeGain, v => _multiChannelPerlin.AmplitudeGain = v, 0, time).SetEase(ease))
                .Join(DOTween.To(() => _multiChannelPerlin.FrequencyGain, v => _multiChannelPerlin.FrequencyGain = v, 0, time).SetEase(ease));
        }

        public void TiltCamera(float tiltValue, float time, Ease ease = Ease.InOutSine)
        {
            if (_tiltTween != null && _tiltTween.IsActive())
                _tiltTween.Kill();
            _tiltTween = CurrentCam.transform.DORotate(new Vector3(0, 0, tiltValue), time).SetEase(ease).SetUpdate(true);
        }

        public void ChangeTarget(Transform target)
        {
            CurrentCam.Follow = target;
        }

        public void CameraOnDead(Action OnComplete)
        {
            TiltCamera(50, 0.7f, Ease.InQuart);
            DOTween.To(() => CurrentCam.Lens.OrthographicSize, v => CurrentCam.Lens.OrthographicSize = v, 3f, 0.7f).SetEase(Ease.InQuart)
                .OnComplete(()=> OnComplete?.Invoke());
        }
    }
}
