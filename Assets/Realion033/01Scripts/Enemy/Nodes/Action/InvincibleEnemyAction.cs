using BSM.Entities;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "InvincibleEnemy", story: "[Health] invincibility is [Value]", category: "Action", id: "66562d3c6a5312de2d3bba6a9f2ab12e")]
public partial class InvincibleEnemyAction : Action
{
    [SerializeReference] public BlackboardVariable<EntityHealth> Health;
    [SerializeReference] public BlackboardVariable<bool> Value;

    protected override Status OnStart()
    {
        Health.Value.isinvincible = Value.Value;
        return Status.Success;
    }
}

