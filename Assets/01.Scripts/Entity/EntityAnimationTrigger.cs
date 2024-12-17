using System;
using UnityEngine;

namespace BSM.Entities
{
    [Flags]
    public enum EAnimationTrigger
    {
        None,
        End,
        Attack,
    }

    public class EntityAnimationTrigger : MonoBehaviour, IEntityComponent
    {
        [SerializeField]
        private EAnimationTrigger _trigger;

        public event Action<EAnimationTrigger> OnAnimationTriggerEvent;

        private Entity _entity;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
        }

        protected virtual void TriggerAnimation(EAnimationTrigger trigger)
        {
            OnAnimationTriggerEvent?.Invoke(trigger);
        }
    }
}
