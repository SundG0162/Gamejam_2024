using BSM.Entities;
using System;
using UnityEngine;

namespace BSM.UI
{
    public class HPBar : Bar, IEntityComponent, IAfterInitializable
    {
        private Entity _entity;
        private EntityHealth _entityHealth;

        public void Initialize(Entity entity)
        {
            _entity = entity;
        }

        public void OnAfterInitialize()
        {
            _entityHealth.OnHealthChangeEvent += HandleOnHealthChangeEvent;
        }

        private void HandleOnHealthChangeEvent(float prevHealth, float currentHealth)
        {
            if(prevHealth > currentHealth)
            {

            }
        }
    }
}
