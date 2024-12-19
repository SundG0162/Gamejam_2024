using System;
using System.Collections.Generic;
using UnityEngine;

namespace SSH.Spawn
{
    
    [CreateAssetMenu(fileName = "WaveInfoSO", menuName = "SO/WaveInfoSO")]
    public class WaveInfoSO : ScriptableObject
    {
        [SerializeField]public SpawnObjectListSO _spawnInfoListSO;
        //[SerializeField]public List<SpawnInfo> _spawnInfos = new List<SpawnInfo>();
    }
}
