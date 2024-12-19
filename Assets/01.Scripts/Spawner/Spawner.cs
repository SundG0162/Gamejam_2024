using System;
using System.Collections;
using System.Collections.Generic;
using BSM;
using BSM.Core.StatSystem;
using BSM.Entities;
using BSM.Players;
using Crogen.CrogenPooling;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SSH.Spawn
{
    public class Spawner : MonoBehaviour
    {
        public List<WaveInfoSO> Waves;
        public StatElementSO[] PropertiesToBeModified;
        private int _currentWave;
        
        PlayerTag playerObject;
        float _amount;   
        private void Start()
        {
            _currentWave = 1;
            _amount = 15;
            playerObject = GameObject.Find("Player").GetComponent<PlayerTag>();
            gameObject.Pop((PoolType.DamageText), transform.position, Quaternion.identity);
            StartCoroutine(SpawnObjects());
        }

        public IEnumerator SpawnObjects()
        {
            while (true)
            {
                foreach (var wave in Waves)
                {
                    foreach (var spawnInfo in wave._spawnInfos)
                    {
                        foreach (var info in spawnInfo._spawnEnemyInfo)
                        {
                            for (int i = 0; i < info.spawnCount; i++)
                            {
                                GameObject g = gameObject.Pop(info.enemyPoolType, GetSpawnPos(), Quaternion.identity).gameObject;
                                EntityStat es = g.GetComponent<EntityStat>();
                                
                                TryModifyStat(es, PropertiesToBeModified[0], _currentWave * 3f);
                                TryModifyStat(es, PropertiesToBeModified[1], _currentWave * 3f);
                                TryModifyStat(es, PropertiesToBeModified[2], _currentWave * 3f);
                                TryModifyStat(es, PropertiesToBeModified[3], _currentWave * 3f);
                                TryModifyStat(es, PropertiesToBeModified[4], _currentWave * 0.3f);
                            }
                        }
                        yield return new WaitForSeconds(spawnInfo._spawnDelay);
                    }
                }
                ++_currentWave;
            }
        }

        public void TryModifyStat(EntityStat EntityStat, StatElementSO stat, float value)
        {
            if (EntityStat.TryGetStatElement(stat, out stat))
            {
                EntityStat.AddModifier(stat, this, value);   
                print($"added value {value}");
            }
        }
        public Vector3 GetSpawnPos()
        {
            float angle = Random.Range(0f, 360f);
            Vector3 _offsetPos = Quaternion.Euler(0,0,angle) * Vector3.down * _amount;
            return _offsetPos + playerObject.transform.position;
        }
    }
}
