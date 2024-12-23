using System;
using System.Collections;
using System.Collections.Generic;
using BSM;
using BSM.Core.StatSystem;
using BSM.Enemies;
using BSM.Entities;
using BSM.Players;
using Crogen.CrogenPooling;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SSH.Spawn
{
    [Serializable]
    public struct SpawnInfo : IComparable<SpawnInfo>
    {
        public PoolType enemyPoolType;
        public int spawnCount;
        public float spawnTime;

        // IComparable<SpawnInfo> 구현
        public int CompareTo(SpawnInfo other)
        {
            // spawnTime을 기준으로 비교 (오름차순)
            if (this.spawnTime < other.spawnTime)
                return -1; // this가 other보다 우선
            else if (this.spawnTime > other.spawnTime)
                return 1;  // this가 other보다 뒤
            else
                return 0;  // 동일한 spawnTime일 경우
        }
    }

    // PriorityQueue 클래스 정의
    public class PriorityQueue<T> where T : IComparable<T>
    {
        private List<T> _elements = new List<T>();

        public int Count => _elements.Count;

        public void Enqueue(T item)
        {
            _elements.Add(item);
            HeapifyUp(_elements.Count - 1);
        }

        public T Dequeue()
        {
            if (_elements.Count == 0)
                throw new InvalidOperationException("Queue is empty.");

            T root = _elements[0];
            _elements[0] = _elements[_elements.Count - 1];
            _elements.RemoveAt(_elements.Count - 1);
            HeapifyDown(0);
            return root;
        }

        public T Peek()
        {
            if (_elements.Count == 0)
                throw new InvalidOperationException("Queue is empty.");
            return _elements[0];
        }

        private void HeapifyUp(int index)
        {
            int parentIndex = (index - 1) / 2;
            if (index > 0 && _elements[index].CompareTo(_elements[parentIndex]) < 0)
            {
                Swap(index, parentIndex);
                HeapifyUp(parentIndex);
            }
        }

        private void HeapifyDown(int index)
        {
            int leftChildIndex = 2 * index + 1;
            int rightChildIndex = 2 * index + 2;
            int smallest = index;

            if (leftChildIndex < _elements.Count && _elements[leftChildIndex].CompareTo(_elements[smallest]) < 0)
                smallest = leftChildIndex;

            if (rightChildIndex < _elements.Count && _elements[rightChildIndex].CompareTo(_elements[smallest]) < 0)
                smallest = rightChildIndex;

            if (smallest != index)
            {
                Swap(index, smallest);
                HeapifyDown(smallest);
            }
        }

        private void Swap(int index1, int index2)
        {
            T temp = _elements[index1];
            _elements[index1] = _elements[index2];
            _elements[index2] = temp;
        }
    }

    public class Spawner : MonoBehaviour
    {
        public StatElementSO[] PropertiesToBeModified;
        private int _currentWave;

        private PriorityQueue<SpawnInfo> spawnQueue = new PriorityQueue<SpawnInfo>();  // 우선순위 큐

        Player playerObject;
        [SerializeField]
        float _amount = 20f;

        private void Start()
        {
            _currentWave = 0;
            PlayerTag tag = GameObject.Find("Player").GetComponent<PlayerTag>();
            tag.OnPlayerChangeEvent += HandleOnPlayerChange;
            playerObject = tag.CurrentPlayer;
            gameObject.Pop((PoolType.DamageText), transform.position, Quaternion.identity);
            StartCoroutine(SpawnObjects());
            StartCoroutine(AddSpawningList());
        }

        private WaitForSeconds _waitFor17Seconds = new WaitForSeconds(17f);
        private WaitForSeconds _waitFor3Seconds = new WaitForSeconds(3f);
        private IEnumerator AddSpawningList()
        {
            while (true)
            {
                yield return new WaitForSeconds(3f);
                for (int i = 0; i < 3; i++)
                {
                    AddEnemyToQueue(1, 3, PoolType.EnemyAlpha, i);
                }

                yield return _waitFor17Seconds;
                yield return _waitFor3Seconds;
                
                for (int i = 0; i < 6; i++)
                {
                    AddEnemyToQueue(1, 3,  PoolType.EnemyAlpha, i);
                }

                yield return _waitFor17Seconds;
                yield return _waitFor3Seconds;
                
                for (int i = 0; i < 6; i++)
                {
                    AddEnemyToQueue(0, 3, PoolType.EnemyAlpha, i);
                }
                int index = Random.Range(0, 3); 
                AddEnemyToQueue(1, 2,  PoolType.EnemyEta, index);

                yield return _waitFor17Seconds;
                ++_currentWave;
            }
        }

        private void AddEnemyToQueue(int minCount, int maxCount, PoolType startPoolType, int scope)
        {
            int count = Random.Range(minCount+_currentWave/2, maxCount+_currentWave);
            float time = Random.Range(0f, 17f);
            SpawnInfo spawnInfo;
            spawnInfo.enemyPoolType = (PoolType)((int)startPoolType + scope);
            spawnInfo.spawnCount = count;
            spawnInfo.spawnTime = time + Time.time;

            spawnQueue.Enqueue(spawnInfo);  // 큐에 추가
        }

        private void HandleOnPlayerChange(Player player)
        {
            playerObject = player;
        }

        public IEnumerator SpawnObjects()
        {
            while (true)
            {
                if (spawnQueue.Count > 0)
                {
                    // 우선순위 큐에서 가장 시간이 빠른 항목을 가져오기
                    var nextSpawn = spawnQueue.Peek();

                    // 현재 시간과 비교하여 해당 spawnTime이 지나면 적을 생성
                    if (Time.time >= nextSpawn.spawnTime)
                    {
                        spawnQueue.Dequeue();  // 큐에서 해당 항목을 빼기

                        // spawnCount만큼 적을 생성
                        for (int i = 0; i < nextSpawn.spawnCount; i++)
                        {
                            SpawnEnemy(nextSpawn);
                        }
                    }
                }

                yield return null;
            }
        }

        private void SpawnEnemy(SpawnInfo info)
        {
            Entity g = gameObject.Pop(info.enemyPoolType, GetSpawnPos(), Quaternion.identity) as BTEnemy;
            EntityStat es = g.GetEntityComponent<EntityStat>();

            TryModifyStat(es, PropertiesToBeModified[0], _currentWave * 3f);
            TryModifyStat(es, PropertiesToBeModified[1], _currentWave * 3f);
            TryModifyStat(es, PropertiesToBeModified[2], _currentWave * 3f);
        }

        public void TryModifyStat(EntityStat EntityStat, StatElementSO stat, float value)
        {
            if (EntityStat.TryGetStatElement(stat, out stat))
            {
                EntityStat.AddModifier(stat, this, value);
            }
        }

        public Vector3 GetSpawnPos()
        {
            float angle = Random.Range(0f, 360f);
            Vector3 _offsetPos = Random.insideUnitCircle.normalized * _amount;
            return _offsetPos + playerObject.transform.position;
        }
    }
}

