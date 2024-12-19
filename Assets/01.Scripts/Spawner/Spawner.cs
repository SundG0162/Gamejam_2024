using System;
using System.Collections;
using System.Collections.Generic;
using BSM;
using UnityEngine;

namespace SSH.Spawn
{
    public class Spawner : MonoBehaviour
    {
        public List<WaveInfoSO> Waves;

        private void Start()
        {
            StartCoroutine(SpawnObjects());
        }

        public IEnumerator SpawnObjects()
        {
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
                           Instantiate(wave._spawnInfoListSO.SpawnObject[i]);
                        }
                    }

                    yield return new WaitForSeconds(spawnInfo._spawnDelay);
                }
            }

            var lastSpawnInfo = Waves[9]._spawnInfos;
            while (true)
            {
                foreach (var spawnInfo in lastSpawnInfo)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < spawnInfo._spawnCount[i]; j++)
                        {
                            print("spawned");
                            print(Time.time);
                            Instantiate(Waves[0]._spawnInfoListSO.SpawnObject[i]);
                        }
                    }
                    yield return new WaitForSeconds(spawnInfo._spawnDelay);
                }
            }
        }
    }
}
