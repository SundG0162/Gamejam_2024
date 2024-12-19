using System.Collections;
using BSM.Entities;
using UnityEngine;

namespace BSM.Enemies
{
    public class MeleeEnemy : BTEnemy
    {
        private EntityRenderer _renderer;
        private EntityMover _mover;
        //private readonly int _dissolveAmountID = Shader.PropertyToID("_DissoleAmount");

        protected override void Awake()
        {
            base.Awake();

            _health = GetComponent<EntityHealth>();
            _mover = GetComponent<EntityMover>();
            _renderer = GetComponentInChildren<EntityRenderer>();
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

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 1.2f);
        }

    }
}
