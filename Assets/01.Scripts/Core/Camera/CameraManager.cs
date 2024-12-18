using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

namespace BSM.Core.Cameras
{
    public class CameraManager : MonoSingleton<CameraManager>
    {
        [field: SerializeField]
        public CinemachineCamera CurrentCam { get; private set; }
        private CinemachineBasicMultiChannelPerlin _multiChannelPerlin;

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
    }
}
