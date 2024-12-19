using BSM.Core.StatSystem;
using BSM.Enemies;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using BSM.Entities;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "HealthEnemy", story: "[Health] [Enemy] is [Value]", category: "Action", id: "44e7965adacbc93f70ff0b34a9c885f8")]
public partial class HealthEnemyAction : Action
{
    [SerializeReference] public BlackboardVariable<StatElementSO> Health;
    [SerializeReference] public BlackboardVariable<BTEnemy> Enemy;
    [SerializeReference] public BlackboardVariable<float> Value;

    protected override Status OnStart()
    {
        Enemy.Value.GetComponent<EntityHealth>().Heal(Value.Value);
        return Status.Success;
    }
}

