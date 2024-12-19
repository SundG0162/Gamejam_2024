using System;
using System.Collections.Generic;
using UnityEngine;

namespace SSH.Spawn
{
    [Serializable]
    public struct SpawnEnemyPair
    {
        public PoolType enemyPoolType;
        public int spawnCount;
    }
    [Serializable]
    public struct SpawnInfo
    {
        public List<SpawnEnemyPair> _spawnEnemyInfo;
        public float _spawnDelay;
    }
    
    [CreateAssetMenu(fileName = "WaveInfoSO", menuName = "SO/WaveInfoSO")]
    public class WaveInfoSO : ScriptableObject
    {
        [SerializeField]public SpawnObjectListSO _spawnInfoListSO;
        [SerializeField]public List<SpawnInfo> _spawnInfos = new List<SpawnInfo>();
    }
}
