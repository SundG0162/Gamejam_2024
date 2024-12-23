using Crogen.CrogenPooling;
using UnityEngine;

namespace BSM.Core.Audios
{
    public class AudioPlayer : MonoBehaviour, IPoolingObject
    {
        public PoolType OriginPoolType { get; set; }
        GameObject IPoolingObject.gameObject { get; set; }

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void Initialize(AudioClip clip, bool isRepeat)
        {
            _audioSource.clip = clip;
            _audioSource.loop = isRepeat;
            _audioSource.Play();
        }

        private void Update()
        {
            if (!_audioSource.isPlaying)
            {
                this.Push();
            }
        }

        public void OnPop()
        {
        }

        public void OnPush()
        {
            _audioSource.Stop();
        }
    }
}
