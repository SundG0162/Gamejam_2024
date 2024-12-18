using BSM.Entities;
using UnityEngine;
using Unity.Behavior;

namespace BSM.Enemies
{
    public class BTEnemy : Entity
    {
        [SerializeField] protected LayerMask _whatIsTarget;
        protected BehaviorGraphAgent _btAgent;

        protected override void Awake()
        {
            base.Awake();
            _btAgent = GetComponent<BehaviorGraphAgent>();
        }

        public BlackboardVariable<T> GetVariable<T>(string variableName)
        {
            if (_btAgent.GetVariable(variableName, out BlackboardVariable<T> variable))
            {
                return variable;
            }
            return null;
        }

        public void SetVariable<T>(string variableName, T value)
        {
            BlackboardVariable<T> variable = GetVariable<T>(variableName);
            Debug.Assert(variable != null, $"Variable {variableName} not found");
            variable.Value = value;
        }

        public Transform GetTargetInRadius(float radius)
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, radius, _whatIsTarget);
            return collider != null ? collider.transform : null;
        }
    }
}
