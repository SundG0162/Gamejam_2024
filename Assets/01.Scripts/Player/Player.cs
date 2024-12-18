using BSM.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BSM.Entities
{
    public class Player : Entity
    {
        [field: SerializeField]
        public InputReaderSO InputReader { get; private set; }

        private void Update()
        {
            if(Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                GetEntityComponent<EntityHealth>().ApplyDamage(this, 5, false, 0);
            }
        }
    }
}
