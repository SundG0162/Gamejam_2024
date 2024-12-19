using System;
using System.Collections;
using System.Collections.Generic;
using BSM;
using BSM.Core.StatSystem;
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
                ++_currentWave;
                foreach (var wave in Waves)
                {
                    foreach (var spawnInfo in wave._spawnInfos)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < spawnInfo._spawnCount[i]; j++)
                            {
                                print("spawned");
                                print(Time.time);
                                GameObject g = Instantiate(wave._spawnInfoListSO.SpawnObject[i]);
                                g.transform.position = GetSpawnPos();
                                
                            }
                        }

                        yield return new WaitForSeconds(spawnInfo._spawnDelay);
                    }
                }
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
