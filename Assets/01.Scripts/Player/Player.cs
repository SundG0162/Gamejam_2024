using BSM.Inputs;
using UnityEngine;

namespace BSM.Entities
{
    public class Player : Entity
    {
        [field: SerializeField]
        public InputReaderSO InputReader { get; private set; }
    }
}
