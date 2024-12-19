using BSM.Core.StatSystem;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using BSM.Enemies;
using BSM.Entities;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ModifierStat", story: "[Enemy] get [Stat] Modifie [value]", category: "Action", id: "80d0bb78f576642e478968109a88c451")]
public partial class ModifierStatAction : Action
{
    [SerializeReference] public BlackboardVariable<StatElementSO> Stat;
    [SerializeReference] public BlackboardVariable<BTEnemy> Enemy;
    [SerializeReference] public BlackboardVariable<float> Value;

    protected override Status OnStart()
    {
        Enemy.Value.GetComponent<EntityStat>().AddModifier(Stat, this, Value.Value);
        return Status.Success;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

