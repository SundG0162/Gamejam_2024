using System.Collections.Generic;
using UnityEngine;

namespace SSH.Spawn
{
    [CreateAssetMenu(fileName = "SpawnObjectListSO", menuName = "SO/SpawnObjectListSO")]
    public class SpawnObjectListSO : ScriptableObject
    {
        public List<GameObject> SpawnObject;
    }
}
