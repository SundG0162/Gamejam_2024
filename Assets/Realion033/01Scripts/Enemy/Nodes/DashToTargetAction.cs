using BSM.Entities;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DashToTarget", story: "Dash a [DashPower] distance between [Target] and [Self] using the [Mover]", category: "Action", id: "8bb3e41685bf2c307c01e6302c54cddc")]
public partial class DashToTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<float> DashPower;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<EntityMover> Mover;

    protected override Status OnStart()
    {
        Vector3 direction = (Target.Value.position - Self.Value.transform.position).normalized;
        Mover.Value.CanManualMove = false;
        Mover.Value.AddForce(direction * DashPower.Value, ForceMode2D.Impulse);
        return Status.Success;
    }
}

