using Crogen.CrogenPooling;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BSM.Core.Audios
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        public AudioBaseSO audioBase;
        public AudioPair[] audioPairs;
        private Dictionary<string, AudioClip> _audioDictionary;

        private void Awake()
        {
            AddAudiosToDictionary();
        }

        private void AddAudiosToDictionary()
        {
            _audioDictionary = new Dictionary<string, AudioClip>();
            audioBase.pairs.ToList().ForEach(pair => 
            { 
                _audioDictionary.Add(pair.key, pair.clip);
            });
        }

        public void PlayAudio(string key, bool isRepeat = false)
        {
            AudioPlayer player = gameObject.Pop(PoolType.AudioPlayer, null) as AudioPlayer;
            player.Initialize(_audioDictionary[key], isRepeat);
        }
    }
}
