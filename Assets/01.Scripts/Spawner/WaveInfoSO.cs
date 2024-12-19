using System;
using System.Collections.Generic;
using UnityEngine;

namespace SSH.Spawn
{
    [Serializable]
    public struct SpawnInfo
    {
        public List<int> _spawnCount;
        public float _spawnDelay;
    }
    
    [CreateAssetMenu(fileName = "WaveInfoSO", menuName = "SO/WaveInfoSO")]
    public class WaveInfoSO : ScriptableObject
    {
        [SerializeField]public SpawnObjectListSO _spawnInfoListSO;
        [SerializeField]public List<SpawnInfo> _spawnInfos = new List<SpawnInfo>();
    }
}
