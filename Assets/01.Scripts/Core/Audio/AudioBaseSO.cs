using Crogen.CrogenPooling;
using UnityEngine;

namespace BSM.Core.Audios
{
    [System.Serializable]
    public class AudioPair
    {
        public string key;
        public AudioClip clip;
    }

    [CreateAssetMenu(fileName = "AudioBaseSO", menuName = "SO/Core/Audios/AudioBaseSO")]
    public class AudioBaseSO : ScriptableObject
    {
        public AudioPair[] pairs;
    }
}
