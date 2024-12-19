using System.Collections;
using BSM.Entities;
using UnityEngine;

namespace BSM.Enemies
{
    public class DashEnemy : BTEnemy
    {        private EntityRenderer _renderer;
        private EntityMover _mover;
        private DashEnemy _meleeEnemy;
        //private readonly int _dissolveAmountID = Shader.PropertyToID("_DissoleAmount");

        protected override void Awake()
        {
            base.Awake();

            _health = GetComponent<EntityHealth>();
            _mover = GetComponent<EntityMover>();
            _renderer = GetComponentInChildren<EntityRenderer>();

            _meleeEnemy = this;
            _health.OnDeadEvent += HandleDeadEvt;
        }


        public override void HandleDeadEvt()
        {
            base.HandleDeadEvt();
            
            _btAgent.enabled = false;

            _hpBar.SetActive(false);
            _mover.StopImmediately();
            
            _renderer.Dissolve(0f, 2.5f);

            StartCoroutine(WaitDieTime(2.5f));
        }

        IEnumerator WaitDieTime(float time)
        {
            yield return new WaitForSeconds(time);
            Destroy(gameObject);
        }

    }
}
