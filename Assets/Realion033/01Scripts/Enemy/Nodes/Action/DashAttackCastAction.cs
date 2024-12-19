using BSM.Enemies;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DashAttackCast", story: "[Enemy] isAttack [Value]", category: "Action", id: "b996e303afd0e92d4c3e7d0f01790dd6")]
public partial class DashAttackCastAction : Action
{
    [SerializeReference] public BlackboardVariable<BTEnemy> Enemy;
    [SerializeReference] public BlackboardVariable<bool> Value;

    protected override Status OnStart()
    {
        Enemy.Value.isDashAttack = Value.Value;
        return Status.Success;
    }
}

