using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using BSM.Enemies;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MeleeAttack", story: "[Self] attack [Player] in [AttackRange]", category: "Action", id: "5856d3b217d1501c675119d91169ba84")]
public partial class MeleeAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Transform> Player;
    [SerializeReference] public BlackboardVariable<float> AttackRange;

    private GameObject _selfObject;
    private Transform _playerTransform;
    private float _attackRange;

    protected override Status OnStart()
    {
        // Get actual values from the Blackboard
        _selfObject = Self.Value;
        _playerTransform = Player.Value;
        _attackRange = AttackRange.Value;

        if (_selfObject == null || _playerTransform == null)
        {
            Debug.LogWarning("MeleeAttackAction: Self or Player is not set.");
            return Status.Failure;
        }

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (_selfObject == null || _playerTransform == null) return Status.Failure;

        // Check if the player is within the attack range
        Collider2D hit = Physics2D.OverlapCircle(_selfObject.transform.position, _attackRange, LayerMask.GetMask("Player"));

        if (hit != null)
        {
            return Status.Success;
        }

        Debug.Log("Player is not in range.");
        return Status.Success;
    }
}

